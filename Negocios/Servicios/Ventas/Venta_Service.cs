using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;

namespace Nk_Colletion_New.Negocios.Servicios.Ventas
{
    public sealed record DetalleVentaTemporal(int IdVariante, string Producto, string Codigo, int Cantidad, decimal PrecioUnitario, int StockDisponible, string? Talla, string? Color)
    {
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    public sealed record DatosPagoVenta(int IdMetodoPago, decimal Monto);

    public class Venta_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Venta_Service(DbContextOptions<NkCollectionContext> options) => _options = options;

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Clientes.AsNoTracking().Where(c => c.Estado != false).OrderBy(c => c.Nombre).ThenBy(c => c.Apellido).ToListAsync();
        }

        public async Task<List<ProductoVariante>> BuscarVariantesAsync(string texto)
        {
            await using var contexto = new NkCollectionContext(_options);
            var consulta = contexto.ProductoVariantes.AsNoTracking()
                .Include(v => v.IdProductoNavigation).ThenInclude(p => p.IdMarcaNavigation)
                .Include(v => v.IdTallaNavigation)
                .Include(v => v.IdColorNavigation)
                .Where(v => v.Estado != false && v.IdProductoNavigation.Estado != false && v.StockActual > 0);
            if (!string.IsNullOrWhiteSpace(texto))
            {
                string termino = texto.Trim();
                consulta = consulta.Where(v => EF.Functions.ILike(v.Codigo, $"%{termino}%") || EF.Functions.ILike(v.IdProductoNavigation.NombreProducto, $"%{termino}%"));
            }

            return await consulta.OrderBy(v => v.IdProductoNavigation.NombreProducto).ThenBy(v => v.IdVariante).Take(100).ToListAsync();
        }

        public async Task<List<MetodoPago>> ListarMetodosPagoAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.MetodoPagos.AsNoTracking().Where(m => m.Estado != false).OrderBy(m => m.Nombre).ToListAsync();
        }

        public async Task<int> GuardarAsync(int idUsuario, int? idCliente, decimal descuento, decimal iva, IEnumerable<DetalleVentaTemporal> detalles, IEnumerable<DatosPagoVenta> pagos)
        {
            var lineas = detalles.ToList();
            var pagosValidos = pagos.Where(p => p.Monto > 0).ToList();
            if (lineas.Count == 0) throw new InvalidOperationException("Agregue al menos un producto a la venta.");
            if (lineas.Any(d => d.Cantidad <= 0 || d.PrecioUnitario < 0)) throw new InvalidOperationException("Revise la cantidad y el precio de los productos.");
            if (descuento < 0 || iva < 0) throw new InvalidOperationException("Descuento e impuesto no pueden ser negativos.");
            decimal subtotal = lineas.Sum(d => d.Subtotal);
            if (descuento > subtotal) throw new InvalidOperationException("El descuento no puede exceder el subtotal.");
            decimal total = subtotal + iva - descuento;
            if (total <= 0) throw new InvalidOperationException("El total de la venta debe ser mayor que cero.");
            if (pagosValidos.Count == 0 || pagosValidos.Sum(p => p.Monto) < total) throw new InvalidOperationException("Los pagos no cubren el total de la venta.");

            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();
            if (idCliente.HasValue && !await contexto.Clientes.AnyAsync(c => c.IdCliente == idCliente.Value && c.Estado != false))
                throw new InvalidOperationException("El cliente seleccionado no existe o está inactivo.");
            foreach (var pago in pagosValidos)
                if (pago.IdMetodoPago <= 0 || !await contexto.MetodoPagos.AnyAsync(m => m.IdMetodoPago == pago.IdMetodoPago && m.Estado != false))
                    throw new InvalidOperationException("Seleccione un método de pago válido.");

            var apertura = await contexto.AperturaCajas.Include(a => a.IdCajaNavigation)
                .Where(a => a.Estado == true && a.FechaCierre == null && a.IdCajaNavigation.Estado != false && a.IdCajaNavigation.IdUsuario == idUsuario)
                .OrderByDescending(a => a.FechaApertura).FirstOrDefaultAsync();
            if (apertura is null) throw new InvalidOperationException("No hay una caja abierta para registrar la venta.");

            if (lineas.GroupBy(d => d.IdVariante).Any(g => g.Count() > 1)) throw new InvalidOperationException("La venta contiene variantes duplicadas.");
            var ids = lineas.Select(d => d.IdVariante).Distinct().ToList();
            var variantes = await contexto.ProductoVariantes.Where(v => ids.Contains(v.IdVariante)).ToDictionaryAsync(v => v.IdVariante);
            foreach (var detalle in lineas)
            {
                if (!variantes.TryGetValue(detalle.IdVariante, out var variante) || variante.Estado == false || variante.StockActual < detalle.Cantidad)
                    throw new InvalidOperationException($"Stock insuficiente o variante inactiva: {detalle.Producto}.");
            }

            var venta = new Ventum
            {
                IdAperturaCaja = apertura.IdAperturaCaja,
                IdCliente = idCliente,
                FechaVenta = DateTime.Now,
                NumeroComprobante = $"V-{DateTime.Now:yyyyMMddHHmmssfff}",
                Subtotal = subtotal,
                Iva = iva,
                Descuento = descuento,
                TotalVenta = total,
                Estado = true
            };
            contexto.Venta.Add(venta);
            await contexto.SaveChangesAsync();

            foreach (var detalle in lineas)
            {
                var variante = variantes[detalle.IdVariante];
                variante.StockActual -= detalle.Cantidad;
                contexto.DetalleVenta.Add(new DetalleVentum { IdVenta = venta.IdVenta, IdVariante = variante.IdVariante, Cantidad = detalle.Cantidad, PrecioUnitario = detalle.PrecioUnitario });
            }

            foreach (var pago in pagosValidos)
                contexto.PagoVenta.Add(new PagoVentum { IdVenta = venta.IdVenta, IdMetodoPago = pago.IdMetodoPago, Monto = pago.Monto, FechaPago = DateTime.Now });

            await contexto.SaveChangesAsync();
            await transaccion.CommitAsync();
            return venta.IdVenta;
        }

        public async Task<List<Ventum>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Venta.AsNoTracking().Include(v => v.IdClienteNavigation).Include(v => v.DetalleVenta).OrderByDescending(v => v.FechaVenta).Take(500).ToListAsync();
        }

        public async Task AnularAsync(int idVenta)
        {
            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();
            var venta = await contexto.Venta.Include(v => v.DetalleVenta).FirstOrDefaultAsync(v => v.IdVenta == idVenta && v.Estado != false);
            if (venta is null) throw new InvalidOperationException("La venta no existe o ya fue anulada.");
            foreach (var detalle in venta.DetalleVenta)
            {
                var variante = await contexto.ProductoVariantes.FirstOrDefaultAsync(v => v.IdVariante == detalle.IdVariante);
                if (variante is not null) variante.StockActual += detalle.Cantidad;
            }
            venta.Estado = false;
            await contexto.SaveChangesAsync();
            await transaccion.CommitAsync();
        }
    }
}
