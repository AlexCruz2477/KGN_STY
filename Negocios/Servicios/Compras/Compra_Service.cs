using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Compras;

public sealed record DetalleCompraTemporal(
    int IdVariante,
    string Producto,
    string Codigo,
    int Cantidad,
    decimal PrecioCompra,
    decimal PrecioVenta)
{
    public decimal Subtotal => Cantidad * PrecioCompra;
}

public sealed class Compra_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Compra_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<List<Proveedor>> ListarProveedoresAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.Proveedors
            .AsNoTracking()
            .Where(proveedor => proveedor.Estado != false)
            .OrderBy(proveedor => proveedor.Nombre)
            .ToListAsync();
    }

    public async Task<List<ProductoVariante>> ListarVariantesAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.ProductoVariantes
            .AsNoTracking()
            .Include(variante => variante.IdProductoNavigation)
            .Where(variante =>
                variante.Estado != false &&
                variante.IdProductoNavigation.Estado != false)
            .OrderBy(variante => variante.IdProductoNavigation.NombreProducto)
            .ThenBy(variante => variante.Codigo)
            .ToListAsync();
    }

    public async Task<int> GuardarAsync(
        int idProveedor,
        int idUsuario,
        string? factura,
        DateTime fecha,
        decimal impuesto,
        IEnumerable<DetalleCompraTemporal> detalles)
    {
        var lineas = detalles.ToList();
        ValidarCompra(idProveedor, idUsuario, impuesto, lineas);

        decimal subtotal = lineas.Sum(detalle => detalle.Subtotal);
        decimal montoImpuesto = Math.Round(subtotal * impuesto / 100m, 2);

        await using var context = new NkCollectionContext(_options);
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            await ValidarEntidadesRelacionadasAsync(
                context,
                idProveedor,
                idUsuario,
                lineas);

            var idsVariantes = lineas
                .Select(detalle => detalle.IdVariante)
                .Distinct()
                .ToList();

            var variantes = await context.ProductoVariantes
                .Where(variante => idsVariantes.Contains(variante.IdVariante))
                .ToDictionaryAsync(variante => variante.IdVariante);

            var compra = new Compra
            {
                IdProveedor = idProveedor,
                IdUsuario = idUsuario,
                NumeroFactura = string.IsNullOrWhiteSpace(factura)
                    ? null
                    : factura.Trim(),
                FechaCompra = DateTime.SpecifyKind(fecha, DateTimeKind.Unspecified),
                Subtotal = subtotal,
                Impuesto = montoImpuesto,
                Total = subtotal + montoImpuesto,
                Estado = true,
                DetalleCompras = lineas.Select(detalle => new DetalleCompra
                {
                    IdVariante = detalle.IdVariante,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioCompra
                }).ToList()
            };

            foreach (var detalle in lineas)
            {
                if (detalle.PrecioVenta > 0)
                {
                    variantes[detalle.IdVariante].PrecioVenta = detalle.PrecioVenta;
                }
            }

            context.Compras.Add(compra);

            // PostgreSQL incrementa el stock, actualiza el precio de compra,
            // recalcula los totales y registra el kardex con los triggers de
            // detalle_compra. C# solo registra la transacción de negocio.
            await context.SaveChangesAsync();
            await context.Entry(compra).ReloadAsync();
            await transaction.CommitAsync();

            return compra.IdCompra;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<Compra>> ListarAsync()
    {
        await using var context = new NkCollectionContext(_options);

        return await context.Compras
            .AsNoTracking()
            .Include(compra => compra.IdProveedorNavigation)
            .Include(compra => compra.IdUsuarioNavigation)
            .Include(compra => compra.DetalleCompras)
                .ThenInclude(detalle => detalle.IdVarianteNavigation)
            .OrderByDescending(compra => compra.FechaCompra)
            .ToListAsync();
    }

    public async Task<Compra?> ObtenerPorIdAsync(int idCompra)
    {
        if (idCompra <= 0)
        {
            return null;
        }

        await using var context = new NkCollectionContext(_options);

        return await context.Compras
            .AsNoTracking()
            .Include(compra => compra.IdProveedorNavigation)
            .Include(compra => compra.IdUsuarioNavigation)
            .Include(compra => compra.DetalleCompras)
                .ThenInclude(detalle => detalle.IdVarianteNavigation)
            .FirstOrDefaultAsync(compra => compra.IdCompra == idCompra);
    }

    private static void ValidarCompra(
        int idProveedor,
        int idUsuario,
        decimal impuesto,
        IReadOnlyCollection<DetalleCompraTemporal> lineas)
    {
        if (idProveedor <= 0 || idUsuario <= 0)
        {
            throw new ArgumentException(
                "Proveedor o usuario no válido.");
        }

        if (lineas.Count == 0)
        {
            throw new InvalidOperationException(
                "Agregue al menos un producto a la compra.");
        }

        if (lineas.Any(detalle =>
                detalle.IdVariante <= 0 ||
                detalle.Cantidad <= 0 ||
                detalle.PrecioCompra < 0 ||
                detalle.PrecioVenta < 0))
        {
            throw new InvalidOperationException(
                "Revise las variantes, cantidades y precios de compra/venta.");
        }

        if (lineas.GroupBy(detalle => detalle.IdVariante).Any(grupo => grupo.Count() > 1))
        {
            throw new InvalidOperationException(
                "La compra contiene variantes duplicadas.");
        }

        if (impuesto < 0)
        {
            throw new InvalidOperationException(
                "El impuesto no puede ser negativo.");
        }
    }

    private static async Task ValidarEntidadesRelacionadasAsync(
        NkCollectionContext context,
        int idProveedor,
        int idUsuario,
        IReadOnlyCollection<DetalleCompraTemporal> lineas)
    {
        bool proveedorActivo = await context.Proveedors
            .AnyAsync(proveedor =>
                proveedor.IdProveedor == idProveedor &&
                proveedor.Estado != false);

        if (!proveedorActivo)
        {
            throw new InvalidOperationException(
                "El proveedor no existe o está inactivo.");
        }

        bool usuarioActivo = await context.Usuarios
            .AnyAsync(usuario =>
                usuario.IdUsuario == idUsuario &&
                usuario.Estado);

        if (!usuarioActivo)
        {
            throw new InvalidOperationException(
                "El usuario no existe o está inactivo.");
        }

        var idsVariantes = lineas
            .Select(detalle => detalle.IdVariante)
            .Distinct()
            .ToList();

        int variantesActivas = await context.ProductoVariantes
            .CountAsync(variante =>
                idsVariantes.Contains(variante.IdVariante) &&
                variante.Estado != false);

        if (variantesActivas != idsVariantes.Count)
        {
            throw new InvalidOperationException(
                "Hay variantes inexistentes o inactivas en la compra.");
        }
    }
}
