using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Caja;

public sealed class Arqueo_Service
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public Arqueo_Service(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<ArqueoCaja> GuardarAsync(
        int idAperturaCaja,
        string? observacion,
        IEnumerable<DetalleArqueo> detalles)
    {
        var lista = detalles.ToList();

        if (lista.Any(d =>
                d.Cantidad < 0 ||
                d.Denominacion <= 0 ||
                (d.Moneda != "NIO" && d.Moneda != "USD")))
        {
            throw new ArgumentException("Hay denominaciones no válidas en el arqueo.");
        }

        await using var contexto = new NkCollectionContext(_options);
        await using var transaccion = await contexto.Database.BeginTransactionAsync();

        bool aperturaActiva = await contexto.AperturaCajas.AnyAsync(a =>
            a.IdAperturaCaja == idAperturaCaja &&
            a.Estado == true &&
            a.FechaCierre == null);

        if (!aperturaActiva)
        {
            throw new InvalidOperationException("La caja no está abierta.");
        }

        bool existeArqueo = await contexto.ArqueoCajas.AnyAsync(a =>
            a.IdAperturaCaja == idAperturaCaja &&
            a.Estado == true);

        if (existeArqueo)
        {
            throw new InvalidOperationException(
                "Ya existe un arqueo activo para esta apertura de caja.");
        }

        var arqueo = new ArqueoCaja
        {
            IdAperturaCaja = idAperturaCaja,
            FechaArqueo = DateTime.Now,
            Observacion = string.IsNullOrWhiteSpace(observacion)
                ? null
                : observacion.Trim(),
            Estado = true,
            DetalleArqueos = lista
                .Where(d => d.Cantidad > 0)
                .Select(d => new DetalleArqueo
                {
                    Denominacion = d.Denominacion,
                    Cantidad = d.Cantidad,
                    Moneda = d.Moneda
                })
                .ToList()
        };

        contexto.ArqueoCajas.Add(arqueo);
        await contexto.SaveChangesAsync();
        await contexto.Entry(arqueo).ReloadAsync();
        await transaccion.CommitAsync();

        return arqueo;
    }
}
