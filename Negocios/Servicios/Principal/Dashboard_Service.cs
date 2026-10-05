using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Npgsql;

namespace Nk_Colletion_New.Negocios.Servicios.Principal;

public sealed record DashboardResumen(
    int VentasRealizadasHoy,
    int ComprasRealizadasHoy,
    int StockBajo,
    bool CajaAbierta,
    int ProductosActivos,
    int ClientesActivos,
    decimal SaldoCaja,
    IReadOnlyList<VentaDiariaResumen> VentasSemana,
    IReadOnlyList<VentaDiariaResumen> ComprasSemana,
    IReadOnlyList<ProductoMasVendidoResumen> ProductosMasVendidos,
    IReadOnlyList<string> Advertencias);

public sealed record VentaDiariaResumen(DateTime Fecha, decimal Total);

public sealed record ProductoMasVendidoResumen(string Producto, int Cantidad);

public sealed record DashboardResultado(DashboardResumen? Resumen, string? Error, string? ConsultaFallida)
{
    public bool Exitoso => Resumen is not null;
}

public sealed class Dashboard_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Dashboard_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<DashboardResumen> ObtenerAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);
        if (!await contexto.Database.CanConnectAsync())
        {
            throw new InvalidOperationException("No se pudo establecer conexión con la base de datos configurada.");
        }

        var hoy = DateTime.Today;
        var manana = hoy.AddDays(1);
        var inicioSemana = hoy.AddDays(-6);
        var advertencias = new List<string>();

        int ventas = 0;
        try
        {
            ventas = await contexto.Venta.AsNoTracking()
                .Where(v => v.Estado != false && v.FechaVenta >= hoy && v.FechaVenta < manana)
                .CountAsync();
        }
        catch (Exception ex)
        {
            advertencias.Add($"Ventas de hoy: {ex.GetBaseException().Message}");
        }

        int compras = 0;
        try
        {
            compras = await contexto.Compras.AsNoTracking()
                .Where(c => c.Estado != false && c.FechaCompra >= hoy && c.FechaCompra < manana)
                .CountAsync();
        }
        catch (Exception ex)
        {
            advertencias.Add($"Compras del mes: {ex.GetBaseException().Message}");
        }

        int stockBajo = await ConsultarAsync(
            () => contexto.ProductoVariantes.AsNoTracking().CountAsync(v => v.Estado != false && v.StockActual <= v.StockMinimo),
            0, "Stock bajo", advertencias);

        bool cajaAbierta = await ConsultarAsync(
            () => contexto.AperturaCajas.AsNoTracking().AnyAsync(a => a.IdAperturaCaja == idAperturaCaja && a.Estado == true && a.FechaCierre == null),
            false, "Estado de caja", advertencias);

        int productosActivos = await ConsultarAsync(
            () => contexto.Productos.AsNoTracking().CountAsync(p => p.Estado == true),
            0, "Productos activos", advertencias);

        int clientesActivos = await ConsultarAsync(
            () => contexto.Clientes.AsNoTracking().CountAsync(c => c.Estado == true),
            0, "Clientes activos", advertencias);

        decimal saldoCaja = 0m;
        if (idAperturaCaja > 0)
        {
            try
            {
                var apertura = await contexto.AperturaCajas.AsNoTracking()
                    .Where(a => a.IdAperturaCaja == idAperturaCaja)
                    .Select(a => new { a.MontoApertura })
                    .FirstOrDefaultAsync();

                if (apertura is not null)
                {
                    decimal efectivoCaja = await (
                        from pago in contexto.PagoVenta.AsNoTracking()
                        join venta in contexto.Venta.AsNoTracking() on pago.IdVenta equals venta.IdVenta
                        join metodo in contexto.MetodoPagos.AsNoTracking() on pago.IdMetodoPago equals metodo.IdMetodoPago
                        where venta.Estado != false
                              && venta.IdAperturaCaja == idAperturaCaja
                              && EF.Functions.ILike(metodo.Nombre.Trim(), "%efectivo%")
                        select (decimal?)pago.Monto)
                        .SumAsync() ?? 0m;
                    decimal egresosCaja = await contexto.Egresos.AsNoTracking()
                        .Where(e => e.Estado != false && e.IdAperturaCaja == idAperturaCaja)
                        .SumAsync(e => (decimal?)e.Monto) ?? 0m;
                    saldoCaja = (apertura.MontoApertura ?? 0m) + efectivoCaja - egresosCaja;
                }
            }
            catch (Exception ex)
            {
                advertencias.Add($"Saldo de caja: {ex.GetBaseException().Message}");
            }
        }

        var ventasSemana = new List<VentaDiariaResumen>();
        try
        {
            var ventasSemanaFiltradas = await contexto.Venta.AsNoTracking()
                .Where(v => v.Estado != false && v.FechaVenta >= inicioSemana && v.FechaVenta < manana)
                .Select(v => new { Fecha = v.FechaVenta, v.TotalVenta })
                .ToListAsync();
            ventasSemana = ventasSemanaFiltradas
                .Where(v => v.Fecha.HasValue && v.Fecha.Value.Date >= inicioSemana && v.Fecha.Value.Date < manana)
                .GroupBy(v => v.Fecha!.Value.Date)
                .Select(grupo => new VentaDiariaResumen(grupo.Key, grupo.Sum(v => v.TotalVenta)))
                .ToList();
        }
        catch (Exception ex)
        {
            advertencias.Add($"Gráfico semanal: {ex.GetBaseException().Message}");
        }

        var comprasSemana = new List<VentaDiariaResumen>();
        try
        {
            var comprasSemanaFiltradas = await contexto.Compras.AsNoTracking()
                .Where(c => c.Estado != false && c.FechaCompra >= inicioSemana && c.FechaCompra < manana)
                .Select(c => new { Fecha = c.FechaCompra, c.Total })
                .ToListAsync();
            comprasSemana = comprasSemanaFiltradas
                .Where(c => c.Fecha.HasValue && c.Fecha.Value.Date >= inicioSemana && c.Fecha.Value.Date < manana)
                .GroupBy(c => c.Fecha!.Value.Date)
                .Select(grupo => new VentaDiariaResumen(grupo.Key, grupo.Sum(c => c.Total)))
                .ToList();
        }
        catch (Exception ex)
        {
            advertencias.Add($"Gráfico de compras: {ex.GetBaseException().Message}");
        }

        var productosMasVendidos = new List<ProductoMasVendidoResumen>();
        try
        {
            var detallesVendidos = await contexto.DetalleVenta.AsNoTracking()
                .Where(d => d.IdVentaNavigation.Estado != false)
                .Select(d => new
                {
                    Producto = d.IdVarianteNavigation.IdProductoNavigation.NombreProducto,
                    d.Cantidad
                })
                .ToListAsync();
            productosMasVendidos = detallesVendidos
                .GroupBy(d => d.Producto)
                .Select(grupo => new ProductoMasVendidoResumen(grupo.Key, grupo.Sum(d => d.Cantidad)))
                .OrderByDescending(item => item.Cantidad)
                .Take(5)
                .ToList();
        }
        catch (Exception ex)
        {
            advertencias.Add($"Productos más vendidos: {ex.GetBaseException().Message}");
        }

        return new DashboardResumen(ventas, compras, stockBajo, cajaAbierta, productosActivos,
            clientesActivos, saldoCaja, ventasSemana, comprasSemana, productosMasVendidos, advertencias);
    }

    public async Task<DashboardResultado> IntentarObtenerAsync(int idAperturaCaja)
    {
        try
        {
            return new DashboardResultado(await ObtenerAsync(idAperturaCaja), null, null);
        }
        catch (Exception ex)
        {
            return new DashboardResultado(null, ex.GetBaseException().Message,
                ex switch
                {
                    PostgresException postgres => $"PostgreSQL ({postgres.SqlState})",
                    NpgsqlException => "Conexión PostgreSQL",
                    _ => ex.GetType().Name
                });
        }
    }

    private static async Task<T> ConsultarAsync<T>(
        Func<Task<T>> consulta,
        T valorAlternativo,
        string nombre,
        ICollection<string> advertencias)
    {
        try
        {
            return await consulta();
        }
        catch (Exception ex)
        {
            advertencias.Add($"{nombre}: {ex.GetBaseException().Message}");
            return valorAlternativo;
        }
    }
}
