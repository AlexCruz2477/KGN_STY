using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Cambio;

public sealed class TasaCambio_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public TasaCambio_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<TipoCambioBcn> ObtenerAsync()
    {
        await using var contexto = new NkCollectionContext(_options);
        var hoy = DateOnly.FromDateTime(DateTime.Today);

        return await contexto.TipoCambioBcns
            .AsNoTracking()
            .Where(t => t.Oficial && t.Fecha <= hoy)
            .OrderByDescending(t => t.Fecha)
            .FirstOrDefaultAsync()
            ?? throw new InvalidOperationException(
                "No existe una tasa oficial de cambio registrada.");
    }
}
