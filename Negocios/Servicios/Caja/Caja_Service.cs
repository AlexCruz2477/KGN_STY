using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Caja;

public sealed class Caja_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Caja_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<AperturaCaja> AbrirCajaAsync(
        int idUsuario,
        decimal montoApertura)
    {
        if (idUsuario <= 0)
        {
            throw new ArgumentException("El usuario de la sesión no es válido.");
        }

        if (montoApertura < 0)
        {
            throw new ArgumentException("El saldo inicial no puede ser negativo.");
        }

        await using var contexto = new NkCollectionContext(_options);
        await using var transaccion = await contexto.Database.BeginTransactionAsync();

        var caja = await contexto.Cajas
            .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

        if (caja is null)
        {
            caja = new Datos.Modelos.Caja
            {
                IdUsuario = idUsuario,
                NumeroCaja = $"CAJA-{idUsuario:D3}",
                Estado = true
            };

            contexto.Cajas.Add(caja);
            await contexto.SaveChangesAsync();
        }
        else if (caja.Estado == false)
        {
            caja.Estado = true;
            await contexto.SaveChangesAsync();
        }

        var activa = await contexto.AperturaCajas
            .FirstOrDefaultAsync(a =>
                a.IdCaja == caja.IdCaja &&
                a.Estado == true &&
                a.FechaCierre == null);

        if (activa is not null)
        {
            await transaccion.CommitAsync();
            return activa;
        }

        var apertura = new AperturaCaja
        {
            IdCaja = caja.IdCaja,
            FechaApertura = DateTime.Now,
            MontoApertura = montoApertura,
            Estado = true
        };

        contexto.AperturaCajas.Add(apertura);
        await contexto.SaveChangesAsync();
        await transaccion.CommitAsync();
        return apertura;
    }

    public async Task<AperturaCaja?> ObtenerAperturaActivaAsync(int idUsuario)
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.AperturaCajas
            .AsNoTracking()
            .Include(a => a.IdCajaNavigation)
            .ThenInclude(c => c.IdUsuarioNavigation)
            .Where(a =>
                a.Estado == true &&
                a.FechaCierre == null &&
                a.IdCajaNavigation.IdUsuario == idUsuario)
            .OrderByDescending(a => a.FechaApertura)
            .FirstOrDefaultAsync();
    }

    public async Task<AperturaCaja> ObtenerPorIdAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.AperturaCajas
            .AsNoTracking()
            .Include(a => a.IdCajaNavigation)
            .ThenInclude(c => c.IdUsuarioNavigation)
            .FirstOrDefaultAsync(a => a.IdAperturaCaja == idAperturaCaja)
            ?? throw new InvalidOperationException("La apertura de caja no existe.");
    }
}
