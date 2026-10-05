using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Compras
{
    public sealed record DetalleCompraTemporal(int IdVariante, string Producto, string Codigo, int Cantidad, decimal PrecioCompra, decimal PrecioVenta)
    {
        public decimal Subtotal => Cantidad * PrecioCompra;
    }

    public class Compra_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Compra_Service(DbContextOptions<NkCollectionContext> options) => _options = options;

        public async Task<List<Proveedor>> ListarProveedoresAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Proveedors.AsNoTracking().Where(p => p.Estado != false).OrderBy(p => p.Nombre).ToListAsync();
        }

        public async Task<List<ProductoVariante>> ListarVariantesAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.ProductoVariantes.AsNoTracking().Include(v => v.IdProductoNavigation)
                .Where(v => v.Estado != false && v.IdProductoNavigation.Estado != false)
                .OrderBy(v => v.IdProductoNavigation.NombreProducto).ThenBy(v => v.Codigo).ToListAsync();
        }

        public async Task<int> GuardarAsync(int idProveedor, int idUsuario, string? factura, DateTime fecha, decimal impuesto, IEnumerable<DetalleCompraTemporal> detalles)
        {
            var lineas = detalles.ToList();
            if (lineas.Count == 0) throw new InvalidOperationException("Agregue al menos un producto a la compra.");
            if (lineas.Any(d => d.Cantidad <= 0 || d.PrecioCompra < 0 || d.PrecioVenta < 0)) throw new InvalidOperationException("Revise cantidades y precios de compra/venta.");
            if (impuesto < 0) throw new InvalidOperationException("El impuesto no puede ser negativo.");
            decimal subtotal = lineas.Sum(d => d.Subtotal);
            decimal montoImpuesto = Math.Round(subtotal * impuesto / 100m, 2);

            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();
            if (!await contexto.Proveedors.AnyAsync(p => p.IdProveedor == idProveedor && p.Estado != false)) throw new InvalidOperationException("Seleccione un proveedor activo.");
            if (!await contexto.Usuarios.AnyAsync(u => u.IdUsuario == idUsuario && u.Estado)) throw new InvalidOperationException("El usuario no existe o está inactivo.");
            if (lineas.GroupBy(d => d.IdVariante).Any(g => g.Count() > 1)) throw new InvalidOperationException("La compra contiene variantes duplicadas.");
            var ids = lineas.Select(d => d.IdVariante).Distinct().ToList();
            var variantes = await contexto.ProductoVariantes.Where(v => ids.Contains(v.IdVariante)).ToDictionaryAsync(v => v.IdVariante);
            if (variantes.Count != ids.Count || lineas.Any(d => !variantes.ContainsKey(d.IdVariante) || variantes[d.IdVariante].Estado == false))
                throw new InvalidOperationException("Una o más variantes no existen o están inactivas.");

            var compra = new Compra
            {
                IdProveedor = idProveedor,
                IdUsuario = idUsuario,
                NumeroFactura = string.IsNullOrWhiteSpace(factura) ? null : factura.Trim(),
                FechaCompra = fecha,
                Subtotal = subtotal,
                Impuesto = montoImpuesto,
                Total = subtotal + montoImpuesto,
                Estado = true
            };
            contexto.Compras.Add(compra);
            await contexto.SaveChangesAsync();
            foreach (var detalle in lineas)
            {
                var variante = variantes[detalle.IdVariante];
                variante.StockActual += detalle.Cantidad;
                variante.PrecioCompra = detalle.PrecioCompra;
                if (detalle.PrecioVenta > 0) variante.PrecioVenta = detalle.PrecioVenta;
                contexto.DetalleCompras.Add(new DetalleCompra { IdCompra = compra.IdCompra, IdVariante = detalle.IdVariante, Cantidad = detalle.Cantidad, PrecioUnitario = detalle.PrecioCompra });
            }
            await contexto.SaveChangesAsync();
            await transaccion.CommitAsync();
            return compra.IdCompra;
        }
    }
}
