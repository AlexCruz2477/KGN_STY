using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using CajaEntidad = Nk_Colletion_New.Datos.Modelos.Caja;

namespace Nk_Colletion_New.Negocios.Servicios.Caja
{
    public class AperturaCaja_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public AperturaCaja_Service(DbContextOptions<NkCollectionContext> options) => _options = options;

        public async Task<(CajaEntidad Caja, AperturaCaja? Apertura)> ObtenerEstadoAsync(int idUsuario)
        {
            await using var contexto = new NkCollectionContext(_options);
            var caja = await contexto.Cajas.AsNoTracking().FirstOrDefaultAsync(c => c.IdUsuario == idUsuario && c.Estado != false);
            if (caja is null) throw new InvalidOperationException("El usuario no tiene una caja activa asignada.");
            var apertura = await contexto.AperturaCajas.AsNoTracking()
                .Where(a => a.IdCaja == caja.IdCaja && a.Estado == true && a.FechaCierre == null)
                .OrderByDescending(a => a.FechaApertura).FirstOrDefaultAsync();
            return (caja, apertura);
        }

        public async Task<int> AbrirAsync(int idUsuario, decimal montoApertura)
        {
            if (montoApertura < 0) throw new InvalidOperationException("El fondo inicial no puede ser negativo.");
            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();
            var caja = await contexto.Cajas.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario && c.Estado != false);
            if (caja is null) throw new InvalidOperationException("El usuario no tiene una caja activa asignada.");
            bool yaAbierta = await contexto.AperturaCajas.AnyAsync(a => a.IdCaja == caja.IdCaja && a.Estado == true && a.FechaCierre == null);
            if (yaAbierta) throw new InvalidOperationException("Esta caja ya tiene una apertura vigente.");
            var apertura = new AperturaCaja { IdCaja = caja.IdCaja, FechaApertura = DateTime.Now, MontoApertura = montoApertura, Estado = true };
            contexto.AperturaCajas.Add(apertura);
            await contexto.SaveChangesAsync();
            await transaccion.CommitAsync();
            return apertura.IdAperturaCaja;
        }
    }
}
