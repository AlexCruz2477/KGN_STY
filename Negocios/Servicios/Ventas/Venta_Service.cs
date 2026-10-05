using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Ventas;

public sealed record DetalleVentaTemporal(
    int IdVariante,
    string Producto,
    string Codigo,
    int Cantidad,
    decimal PrecioUnitario,
    int StockDisponible,
    string? Talla,
    string? Color)
{
    public decimal Subtotal => Cantidad * PrecioUnitario;
}

public sealed record DatosPagoVenta(int IdMetodoPago, decimal Monto);

public sealed class Venta_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Venta_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<List<Cliente>> ListarClientesAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.Clientes
            .AsNoTracking()
            .Where(cliente => cliente.Estado != false)
            .OrderBy(cliente => cliente.Nombre)
            .ThenBy(cliente => cliente.Apellido)
            .ToListAsync();
    }

    public async Task<List<ProductoVariante>> BuscarVariantesAsync(string texto)
    {
        await using var context = new NkCollectionContext(_options);

        var query = context.ProductoVariantes
            .AsNoTracking()
            .Include(variante => variante.IdProductoNavigation)
                .ThenInclude(producto => producto.IdMarcaNavigation)
            .Include(variante => variante.IdProductoNavigation)
                .ThenInclude(producto => producto.IdCategoriaNavigation)
            .Include(variante => variante.IdTallaNavigation)
            .Include(variante => variante.IdColorNavigation)
            .Where(variante =>
                variante.Estado != false &&
                variante.IdProductoNavigation.Estado != false &&
                variante.StockActual > 0);

        if (!string.IsNullOrWhiteSpace(texto))
        {
            string termino = texto.Trim();
            query = query.Where(variante =>
                EF.Functions.ILike(variante.Codigo, $"%{termino}%") ||
                EF.Functions.ILike(variante.IdProductoNavigation.NombreProducto, $"%{termino}%"));
        }

        return await query
            .OrderBy(variante => variante.IdProductoNavigation.NombreProducto)
            .ThenBy(variante => variante.IdVariante)
            .Take(100)
            .ToListAsync();
    }

    public async Task<List<MetodoPago>> ListarMetodosPagoAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.MetodoPagos
            .AsNoTracking()
            .Where(metodo => metodo.Estado != false)
            .OrderBy(metodo => metodo.Nombre)
            .ToListAsync();
    }

    public async Task<int> ObtenerMetodoPagoIdAsync(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Método de pago no válido.", nameof(nombre));
        }

        string termino = nombre.Trim();
        await using var context = new NkCollectionContext(_options);

        var metodo = await context.MetodoPagos
            .AsNoTracking()
            .FirstOrDefaultAsync(item =>
                item.Estado != false &&
                EF.Functions.ILike(item.Nombre, termino));

        if (metodo is null)
        {
            throw new InvalidOperationException(
                $"No existe el método de pago '{termino}'.");
        }

        return metodo.IdMetodoPago;
    }

    public async Task<int> GuardarAsync(
        int idUsuario,
        int? idCliente,
        decimal descuento,
        decimal iva,
        IEnumerable<DetalleVentaTemporal> detalles,
        IEnumerable<DatosPagoVenta> pagos)
    {
        var lineas = detalles.ToList();
        var pagosValidos = pagos.Where(pago => pago.Monto > 0).ToList();

        ValidarVenta(lineas, pagosValidos, descuento, iva);

        decimal subtotal = lineas.Sum(detalle => detalle.Subtotal);
        decimal total = subtotal - descuento + iva;

        await using var context = new NkCollectionContext(_options);
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            await ValidarEntidadesRelacionadasAsync(
                context,
                idUsuario,
                idCliente,
                lineas,
                pagosValidos);

            var apertura = await context.AperturaCajas
                .Include(apertura => apertura.IdCajaNavigation)
                .Where(apertura =>
                    apertura.Estado == true &&
                    apertura.FechaCierre == null &&
                    apertura.IdCajaNavigation.Estado != false &&
                    apertura.IdCajaNavigation.IdUsuario == idUsuario)
                .OrderByDescending(apertura => apertura.FechaApertura)
                .FirstOrDefaultAsync();

            if (apertura is null)
            {
                throw new InvalidOperationException(
                    "No hay una caja abierta para registrar la venta.");
            }

            var venta = new Ventum
            {
                IdAperturaCaja = apertura.IdAperturaCaja,
                IdCliente = idCliente,
                FechaVenta = DateTime.Now,
                NumeroComprobante = null,
                Subtotal = subtotal,
                Iva = iva,
                Descuento = descuento,
                TotalVenta = total,
                Estado = true,
                DetalleVenta = lineas.Select(detalle => new DetalleVentum
                {
                    IdVariante = detalle.IdVariante,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario
                }).ToList()
            };

            context.Venta.Add(venta);

            // PostgreSQL descuenta el inventario y recalcula los totales mediante
            // los triggers definidos para detalle_venta. No se modifica el stock
            // manualmente aquí para evitar aplicar el movimiento dos veces.
            await context.SaveChangesAsync();

            venta.NumeroComprobante = $"V-{venta.IdVenta:D6}";

            foreach (var pago in pagosValidos)
            {
                context.PagoVenta.Add(new PagoVentum
                {
                    IdVenta = venta.IdVenta,
                    IdMetodoPago = pago.IdMetodoPago,
                    Monto = pago.Monto,
                    FechaPago = DateTime.Now
                });
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return venta.IdVenta;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Ventum> GuardarVentaAsync(
        Ventum venta,
        List<PagoVentum> pagos)
    {
        ArgumentNullException.ThrowIfNull(venta);

        if (venta.DetalleVenta.Count == 0)
        {
            throw new InvalidOperationException(
                "Agregue al menos un producto a la venta.");
        }

        await using var context = new NkCollectionContext(_options);
        var apertura = await context.AperturaCajas
            .AsNoTracking()
            .Include(item => item.IdCajaNavigation)
            .FirstOrDefaultAsync(item =>
                item.IdAperturaCaja == venta.IdAperturaCaja &&
                item.Estado == true &&
                item.FechaCierre == null);

        if (apertura is null)
        {
            throw new InvalidOperationException(
                "La caja indicada no está abierta.");
        }

        var detalles = venta.DetalleVenta.Select(detalle =>
            new DetalleVentaTemporal(
                detalle.IdVariante,
                $"Variante {detalle.IdVariante}",
                string.Empty,
                detalle.Cantidad,
                detalle.PrecioUnitario,
                0,
                null,
                null));

        var datosPagos = pagos.Select(pago =>
            new DatosPagoVenta(pago.IdMetodoPago, pago.Monto));

        int idVenta = await GuardarAsync(
            apertura.IdCajaNavigation.IdUsuario,
            venta.IdCliente,
            venta.Descuento,
            venta.Iva,
            detalles,
            datosPagos);

        return await ObtenerVentaPorIdAsync(idVenta)
            ?? throw new InvalidOperationException(
                "La venta se guardó, pero no pudo volver a cargarse.");
    }

    public async Task<List<Ventum>> ListarAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AsNoTracking()
            .Include(venta => venta.IdClienteNavigation)
            .Include(venta => venta.IdAperturaCajaNavigation)
            .Include(venta => venta.DetalleVenta)
            .Include(venta => venta.PagoVenta)
                .ThenInclude(pago => pago.IdMetodoPagoNavigation)
            .OrderByDescending(venta => venta.FechaVenta)
            .Take(500)
            .ToListAsync();
    }

    public async Task<Ventum?> ObtenerVentaPorIdAsync(int idVenta)
    {
        if (idVenta <= 0)
        {
            return null;
        }

        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AsNoTracking()
            .Include(venta => venta.IdClienteNavigation)
            .Include(venta => venta.IdAperturaCajaNavigation)
            .Include(venta => venta.DetalleVenta)
                .ThenInclude(detalle => detalle.IdVarianteNavigation)
            .Include(venta => venta.PagoVenta)
                .ThenInclude(pago => pago.IdMetodoPagoNavigation)
            .FirstOrDefaultAsync(venta => venta.IdVenta == idVenta);
    }

    public async Task<Ventum?> BuscarPorComprobanteAsync(string numeroComprobante)
    {
        if (string.IsNullOrWhiteSpace(numeroComprobante))
        {
            return null;
        }

        string comprobante = numeroComprobante.Trim();
        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AsNoTracking()
            .Include(venta => venta.IdClienteNavigation)
            .Include(venta => venta.DetalleVenta)
            .Include(venta => venta.PagoVenta)
                .ThenInclude(pago => pago.IdMetodoPagoNavigation)
            .FirstOrDefaultAsync(venta => venta.NumeroComprobante == comprobante);
    }

    public async Task<List<Ventum>> ObtenerVentasPorFechaAsync(DateTime fecha)
    {
        DateTime inicio = fecha.Date;
        DateTime fin = inicio.AddDays(1);

        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AsNoTracking()
            .Include(venta => venta.IdClienteNavigation)
            .Include(venta => venta.PagoVenta)
            .Where(venta =>
                venta.FechaVenta >= inicio &&
                venta.FechaVenta < fin &&
                venta.Estado == true)
            .OrderByDescending(venta => venta.FechaVenta)
            .ToListAsync();
    }

    public async Task<string> GenerarNumeroComprobanteAsync()
    {
        await using var context = new NkCollectionContext(_options);
        int ultimoId = await context.Venta
            .MaxAsync(venta => (int?)venta.IdVenta)
            ?? 0;

        return $"V-{ultimoId + 1:D6}";
    }

    public async Task<int> ObtenerAperturaCajaActivaAsync(int? idUsuario = null)
    {
        await using var context = new NkCollectionContext(_options);

        var query = context.AperturaCajas
            .AsNoTracking()
            .Include(apertura => apertura.IdCajaNavigation)
            .Where(apertura =>
                apertura.Estado == true &&
                apertura.FechaCierre == null);

        if (idUsuario.HasValue)
        {
            query = query.Where(apertura =>
                apertura.IdCajaNavigation.IdUsuario == idUsuario.Value);
        }

        var apertura = await query
            .OrderByDescending(item => item.FechaApertura)
            .FirstOrDefaultAsync();

        return apertura?.IdAperturaCaja
            ?? throw new InvalidOperationException(
                "No existe una caja abierta. Realice una apertura antes de vender.");
    }

    public Task<List<Ventum>> ObtenerVentasAsync() => ListarAsync();

    public async Task<List<Ventum>> ObtenerVentasActivasAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AsNoTracking()
            .Include(venta => venta.IdClienteNavigation)
            .Where(venta => venta.Estado == true)
            .OrderByDescending(venta => venta.FechaVenta)
            .ToListAsync();
    }

    public async Task<bool> ExisteComprobanteAsync(string numeroComprobante)
    {
        if (string.IsNullOrWhiteSpace(numeroComprobante))
        {
            return false;
        }

        string comprobante = numeroComprobante.Trim();
        await using var context = new NkCollectionContext(_options);

        return await context.Venta
            .AnyAsync(venta => venta.NumeroComprobante == comprobante);
    }

    public async Task<bool> AnularVentaAsync(int idVenta)
    {
        if (idVenta <= 0)
        {
            return false;
        }

        await using var context = new NkCollectionContext(_options);
        var venta = await context.Venta
            .FirstOrDefaultAsync(item => item.IdVenta == idVenta);

        if (venta is null || venta.Estado == false)
        {
            return false;
        }

        venta.Estado = false;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task AnularAsync(int idVenta)
    {
        if (idVenta <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(idVenta));
        }

        await using var context = new NkCollectionContext(_options);
        var venta = await context.Venta
            .FirstOrDefaultAsync(item => item.IdVenta == idVenta && item.Estado != false);

        if (venta is null)
        {
            throw new InvalidOperationException("La venta no existe o ya fue anulada.");
        }

        // El trigger de cambio de estado repone el inventario y registra el
        // movimiento correspondiente en el kardex.
        venta.Estado = false;
        await context.SaveChangesAsync();
    }

    private static void ValidarVenta(
        IReadOnlyCollection<DetalleVentaTemporal> lineas,
        IReadOnlyCollection<DatosPagoVenta> pagos,
        decimal descuento,
        decimal iva)
    {
        if (lineas.Count == 0)
        {
            throw new InvalidOperationException(
                "Agregue al menos un producto a la venta.");
        }

        if (lineas.Any(detalle =>
                detalle.IdVariante <= 0 ||
                detalle.Cantidad <= 0 ||
                detalle.PrecioUnitario < 0))
        {
            throw new InvalidOperationException(
                "Revise la variante, cantidad y precio de los productos.");
        }

        if (lineas.GroupBy(detalle => detalle.IdVariante).Any(grupo => grupo.Count() > 1))
        {
            throw new InvalidOperationException(
                "La venta contiene variantes duplicadas.");
        }

        if (descuento < 0 || iva < 0)
        {
            throw new InvalidOperationException(
                "Descuento e impuesto no pueden ser negativos.");
        }

        decimal subtotal = lineas.Sum(detalle => detalle.Subtotal);
        if (descuento > subtotal)
        {
            throw new InvalidOperationException(
                "El descuento no puede exceder el subtotal.");
        }

        decimal total = subtotal - descuento + iva;
        if (total <= 0)
        {
            throw new InvalidOperationException(
                "El total de la venta debe ser mayor que cero.");
        }

        if (pagos.Count == 0 || pagos.Any(pago => pago.Monto <= 0))
        {
            throw new InvalidOperationException(
                "Registre al menos un pago válido.");
        }

        if (pagos.Sum(pago => pago.Monto) < total)
        {
            throw new InvalidOperationException(
                "Los pagos no cubren el total de la venta.");
        }
    }

    private static async Task ValidarEntidadesRelacionadasAsync(
        NkCollectionContext context,
        int idUsuario,
        int? idCliente,
        IReadOnlyCollection<DetalleVentaTemporal> lineas,
        IReadOnlyCollection<DatosPagoVenta> pagos)
    {
        bool usuarioActivo = await context.Usuarios
            .AnyAsync(usuario => usuario.IdUsuario == idUsuario && usuario.Estado);

        if (!usuarioActivo)
        {
            throw new InvalidOperationException(
                "El usuario no existe o está inactivo.");
        }

        if (idCliente.HasValue)
        {
            bool clienteActivo = await context.Clientes
                .AnyAsync(cliente =>
                    cliente.IdCliente == idCliente.Value &&
                    cliente.Estado != false);

            if (!clienteActivo)
            {
                throw new InvalidOperationException(
                    "El cliente seleccionado no existe o está inactivo.");
            }
        }

        var idsMetodos = pagos
            .Select(pago => pago.IdMetodoPago)
            .Distinct()
            .ToList();

        int metodosActivos = await context.MetodoPagos.CountAsync(metodo =>
            idsMetodos.Contains(metodo.IdMetodoPago) &&
            metodo.Estado != false);

        if (metodosActivos != idsMetodos.Count)
        {
            throw new InvalidOperationException(
                "Hay métodos de pago inválidos o inactivos.");
        }

        var idsVariantes = lineas
            .Select(detalle => detalle.IdVariante)
            .Distinct()
            .ToList();

        var variantes = await context.ProductoVariantes
            .AsNoTracking()
            .Where(variante => idsVariantes.Contains(variante.IdVariante))
            .ToDictionaryAsync(variante => variante.IdVariante);

        foreach (var detalle in lineas)
        {
            if (!variantes.TryGetValue(detalle.IdVariante, out var variante) ||
                variante.Estado == false)
            {
                throw new InvalidOperationException(
                    $"La variante de {detalle.Producto} no existe o está inactiva.");
            }

            if (variante.StockActual < detalle.Cantidad)
            {
                throw new InvalidOperationException(
                    $"Stock insuficiente para {detalle.Producto}. " +
                    $"Disponible: {variante.StockActual}.");
            }
        }
    }
}
