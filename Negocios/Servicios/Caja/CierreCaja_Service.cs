using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Caja;

public sealed class CierreCaja_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public CierreCaja_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<(AperturaCaja Apertura, ArqueoCaja? Arqueo)> ObtenerAsync(
        int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        var apertura = await contexto.AperturaCajas
            .AsNoTracking()
            .Include(a => a.IdCajaNavigation)
            .ThenInclude(c => c.IdUsuarioNavigation)
            .FirstOrDefaultAsync(a => a.IdAperturaCaja == idAperturaCaja)
            ?? throw new InvalidOperationException("La apertura no existe.");

        var arqueo = await contexto.ArqueoCajas
            .AsNoTracking()
            .Include(a => a.DetalleArqueos)
            .Where(a =>
                a.IdAperturaCaja == idAperturaCaja &&
                a.Estado == true)
            .OrderByDescending(a => a.FechaArqueo)
            .ThenByDescending(a => a.IdArqueo)
            .FirstOrDefaultAsync();

        return (apertura, arqueo);
    }

    public async Task CerrarAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        var apertura = await contexto.AperturaCajas
            .FirstOrDefaultAsync(a => a.IdAperturaCaja == idAperturaCaja)
            ?? throw new InvalidOperationException("La apertura no existe.");

        if (apertura.Estado != true || apertura.FechaCierre.HasValue)
        {
            throw new InvalidOperationException("La caja ya está cerrada.");
        }

        bool tieneArqueo = await contexto.ArqueoCajas.AnyAsync(a =>
            a.IdAperturaCaja == idAperturaCaja &&
            a.Estado == true);

        if (!tieneArqueo)
        {
            throw new InvalidOperationException(
                "Debe generar un arqueo antes de cerrar la caja.");
        }

        apertura.Estado = false;
        apertura.FechaCierre = DateTime.Now;
        await contexto.SaveChangesAsync();
    }
}
