using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;

namespace Nk_Colletion_New.Negocios.Servicios.Caja;

public sealed record MovimientoCajaResumen(DateTime Fecha, string Tipo, decimal Monto);

public sealed record CajaResumen(
    decimal SaldoInicial,
    decimal TotalVentas,
    decimal TotalEgresos,
    decimal SaldoEfectivoEsperado,
    int CantidadVentas,
    int CantidadEgresos);

public sealed class CajaResumen_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public CajaResumen_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<List<MovimientoCajaResumen>> ObtenerMovimientosAsync(
        int idAperturaCaja,
        int limite = 12)
    {
        await using var contexto = new NkCollectionContext(_options);

        var ventas = await contexto.Venta
            .AsNoTracking()
            .Where(v => v.IdAperturaCaja == idAperturaCaja && v.Estado == true)
            .OrderByDescending(v => v.FechaVenta)
            .Take(limite)
            .Select(v => new MovimientoCajaResumen(
                v.FechaVenta ?? DateTime.MinValue,
                "Venta",
                v.TotalVenta))
            .ToListAsync();

        var egresos = await contexto.Egresos
            .AsNoTracking()
            .Where(e => e.IdAperturaCaja == idAperturaCaja && e.Estado == true)
            .OrderByDescending(e => e.FechaEgreso)
            .Take(limite)
            .Select(e => new MovimientoCajaResumen(
                e.FechaEgreso ?? DateTime.MinValue,
                "Egreso",
                -e.Monto))
            .ToListAsync();

        return ventas
            .Concat(egresos)
            .OrderByDescending(m => m.Fecha)
            .Take(limite)
            .ToList();
    }

    public async Task<CajaResumen> ObtenerAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        var apertura = await contexto.AperturaCajas
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.IdAperturaCaja == idAperturaCaja)
            ?? throw new InvalidOperationException("La apertura de caja no existe.");

        decimal ventas = await contexto.Venta
            .Where(v =>
                v.IdAperturaCaja == idAperturaCaja &&
                v.Estado == true)
            .SumAsync(v => (decimal?)v.TotalVenta)
            ?? 0m;

        int cantidadVentas = await contexto.Venta.CountAsync(v =>
            v.IdAperturaCaja == idAperturaCaja &&
            v.Estado == true);

        decimal egresos = await contexto.Egresos
            .Where(e =>
                e.IdAperturaCaja == idAperturaCaja &&
                e.Estado == true)
            .SumAsync(e => (decimal?)e.Monto)
            ?? 0m;

        int cantidadEgresos = await contexto.Egresos.CountAsync(e =>
            e.IdAperturaCaja == idAperturaCaja &&
            e.Estado == true);

        decimal efectivo = await (
            from pago in contexto.PagoVenta
            join venta in contexto.Venta on pago.IdVenta equals venta.IdVenta
            join metodo in contexto.MetodoPagos on pago.IdMetodoPago equals metodo.IdMetodoPago
            where venta.IdAperturaCaja == idAperturaCaja
                  && venta.Estado == true
                  && metodo.Estado == true
                  && metodo.Nombre.ToLower() == "efectivo"
            select (decimal?)pago.Monto)
            .SumAsync() ?? 0m;

        decimal saldoEsperado = (apertura.MontoApertura ?? 0m) + efectivo - egresos;

        return new CajaResumen(
            apertura.MontoApertura ?? 0m,
            ventas,
            egresos,
            saldoEsperado,
            cantidadVentas,
            cantidadEgresos);
    }
}
