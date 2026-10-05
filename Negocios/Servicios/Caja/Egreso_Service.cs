using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Caja;

public sealed class Egreso_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Egreso_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task GuardarAsync(
        int idAperturaCaja,
        int idTipoEgreso,
        decimal monto,
        string? descripcion)
    {
        ValidarDatos(idAperturaCaja, idTipoEgreso, monto, descripcion);
        descripcion = NormalizarDescripcion(descripcion);

        await using var contexto = new NkCollectionContext(_options);

        bool aperturaActiva = await contexto.AperturaCajas.AnyAsync(apertura =>
            apertura.IdAperturaCaja == idAperturaCaja &&
            apertura.Estado == true &&
            apertura.FechaCierre == null);

        if (!aperturaActiva)
        {
            throw new InvalidOperationException("La caja no se encuentra abierta.");
        }

        await ValidarTipoActivoAsync(contexto, idTipoEgreso);

        contexto.Egresos.Add(new Egreso
        {
            IdAperturaCaja = idAperturaCaja,
            IdTipoEgreso = idTipoEgreso,
            Monto = monto,
            Descripcion = descripcion,
            FechaEgreso = DateTime.Now,
            Estado = true
        });

        // La BD valida nuevamente que la apertura siga activa mediante trigger.
        await contexto.SaveChangesAsync();
    }

    public async Task<List<Egreso>> ListarAsync()
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos
            .AsNoTracking()
            .Include(egreso => egreso.IdTipoEgresoNavigation)
            .OrderByDescending(egreso => egreso.FechaEgreso)
            .ToListAsync();
    }

    public async Task<List<Egreso>> ListarActivosAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos
            .AsNoTracking()
            .Include(egreso => egreso.IdTipoEgresoNavigation)
            .Where(egreso =>
                egreso.IdAperturaCaja == idAperturaCaja &&
                egreso.Estado == true)
            .OrderByDescending(egreso => egreso.FechaEgreso)
            .ToListAsync();
    }

    public async Task<List<Egreso>> BuscarAsync(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return await ListarAsync();
        }

        string termino = texto.Trim();
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos
            .AsNoTracking()
            .Include(egreso => egreso.IdTipoEgresoNavigation)
            .Where(egreso =>
                (egreso.Descripcion != null &&
                 EF.Functions.ILike(egreso.Descripcion, $"%{termino}%")) ||
                EF.Functions.ILike(
                    egreso.IdTipoEgresoNavigation.Nombre,
                    $"%{termino}%"))
            .OrderByDescending(egreso => egreso.FechaEgreso)
            .ToListAsync();
    }

    public async Task<Egreso?> ObtenerPorIdAsync(int idEgreso)
    {
        if (idEgreso <= 0)
        {
            return null;
        }

        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos
            .AsNoTracking()
            .Include(egreso => egreso.IdTipoEgresoNavigation)
            .FirstOrDefaultAsync(egreso => egreso.IdEgreso == idEgreso);
    }

    public async Task EditarAsync(
        int idEgreso,
        int idTipoEgreso,
        decimal monto,
        string? descripcion)
    {
        if (idEgreso <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(idEgreso));
        }

        ValidarDatos(1, idTipoEgreso, monto, descripcion);
        descripcion = NormalizarDescripcion(descripcion);

        await using var contexto = new NkCollectionContext(_options);
        var egreso = await contexto.Egresos
            .FirstOrDefaultAsync(item => item.IdEgreso == idEgreso)
            ?? throw new InvalidOperationException("El egreso no existe.");

        await ValidarTipoActivoAsync(contexto, idTipoEgreso);

        egreso.IdTipoEgreso = idTipoEgreso;
        egreso.Monto = monto;
        egreso.Descripcion = descripcion;

        await contexto.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int idEgreso, bool estado)
    {
        await using var contexto = new NkCollectionContext(_options);
        var egreso = await contexto.Egresos
            .FirstOrDefaultAsync(item => item.IdEgreso == idEgreso)
            ?? throw new InvalidOperationException("El egreso no existe.");

        egreso.Estado = estado;
        await contexto.SaveChangesAsync();
    }

    public Task DesactivarAsync(int idEgreso) =>
        CambiarEstadoAsync(idEgreso, false);

    public Task ActivarAsync(int idEgreso) =>
        CambiarEstadoAsync(idEgreso, true);

    public async Task<int> ObtenerOCrearTipoAsync(string nombre)
    {
        nombre = (nombre ?? string.Empty).Trim();
        if (nombre.Length == 0)
        {
            throw new ArgumentException("Ingrese el tipo de egreso.");
        }

        await using var contexto = new NkCollectionContext(_options);

        var tipo = await contexto.TipoEgresos
            .FirstOrDefaultAsync(item => EF.Functions.ILike(item.Nombre, nombre));

        if (tipo is not null)
        {
            if (tipo.Estado != true)
            {
                tipo.Estado = true;
                await contexto.SaveChangesAsync();
            }

            return tipo.IdTipoEgreso;
        }

        tipo = new TipoEgreso
        {
            Nombre = nombre,
            Estado = true
        };

        contexto.TipoEgresos.Add(tipo);
        await contexto.SaveChangesAsync();
        return tipo.IdTipoEgreso;
    }

    public async Task<decimal> ObtenerTotalEgresadoAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos
            .Where(egreso =>
                egreso.IdAperturaCaja == idAperturaCaja &&
                egreso.Estado == true)
            .SumAsync(egreso => (decimal?)egreso.Monto)
            ?? 0m;
    }

    public async Task<int> ObtenerCantidadEgresosAsync(int idAperturaCaja)
    {
        await using var contexto = new NkCollectionContext(_options);

        return await contexto.Egresos.CountAsync(egreso =>
            egreso.IdAperturaCaja == idAperturaCaja &&
            egreso.Estado == true);
    }

    private static void ValidarDatos(
        int idAperturaCaja,
        int idTipoEgreso,
        decimal monto,
        string? descripcion)
    {
        if (idAperturaCaja <= 0)
        {
            throw new ArgumentException("No existe una apertura de caja válida.");
        }

        if (idTipoEgreso <= 0)
        {
            throw new ArgumentException("Seleccione un tipo de egreso.");
        }

        if (monto <= 0)
        {
            throw new ArgumentException("El monto debe ser mayor que cero.");
        }

        if (NormalizarDescripcion(descripcion)?.Length > 250)
        {
            throw new ArgumentException(
                "La descripción no puede superar los 250 caracteres.");
        }
    }

    private static string? NormalizarDescripcion(string? descripcion) =>
        string.IsNullOrWhiteSpace(descripcion)
            ? null
            : descripcion.Trim();

    private static async Task ValidarTipoActivoAsync(
        NkCollectionContext contexto,
        int idTipoEgreso)
    {
        bool tipoActivo = await contexto.TipoEgresos.AnyAsync(tipo =>
            tipo.IdTipoEgreso == idTipoEgreso &&
            tipo.Estado == true);

        if (!tipoActivo)
        {
            throw new InvalidOperationException(
                "El tipo de egreso no existe o está inactivo.");
        }
    }
}
