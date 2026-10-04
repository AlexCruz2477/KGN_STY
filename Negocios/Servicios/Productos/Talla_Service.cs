using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Servicios.Productos
{
    internal class Talla_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Talla_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR TALLA
        public async Task GuardarAsync(string nombreTalla)
        {
            if (string.IsNullOrWhiteSpace(nombreTalla))
                throw new Exception("Ingrese el nombre de la talla.");

            nombreTalla = nombreTalla.Trim();

            await using var contexto = new NkCollectionContext(_options);
            bool existe = await contexto.Tallas.AnyAsync(t => EF.Functions.ILike(t.NombreTalla, nombreTalla));
            if (existe)
                throw new Exception("La talla ya está registrada.");

            var talla = new Talla
            {
                NombreTalla = nombreTalla,
                Estado = true
            };

            contexto.Tallas.Add(talla);
            await contexto.SaveChangesAsync();
        }

        // LISTAR TALLAS
        public async Task<List<Talla>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Tallas.AsNoTracking().OrderBy(t => t.NombreTalla).ToListAsync();
        }

        // BUSCAR TALLAS
        public async Task<List<Talla>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Tallas.AsNoTracking()
                .Where(t => EF.Functions.ILike(t.NombreTalla, $"%{texto}%"))
                .OrderBy(t => t.NombreTalla)
                .ToListAsync();
        }

        // OBTENER TALLA POR ID
        public async Task<Talla?> ObtenerPorIdAsync(int idTalla)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Tallas.AsNoTracking().FirstOrDefaultAsync(t => t.IdTalla == idTalla);
        }

        // EDITAR TALLA
        public async Task EditarAsync(int idTalla, string nombreTalla)
        {
            if (idTalla <= 0)
                throw new Exception("Talla no válida.");

            if (string.IsNullOrWhiteSpace(nombreTalla))
                throw new Exception("Ingrese el nombre de la talla.");

            nombreTalla = nombreTalla.Trim();

            await using var contexto = new NkCollectionContext(_options);
            var talla = await contexto.Tallas.FirstOrDefaultAsync(t => t.IdTalla == idTalla);
            if (talla == null)
                throw new Exception("La talla no existe.");

            bool existe = await contexto.Tallas.AnyAsync(t => EF.Functions.ILike(t.NombreTalla, nombreTalla) && t.IdTalla != idTalla);
            if (existe)
                throw new Exception("Ya existe otra talla con ese nombre.");

            talla.NombreTalla = nombreTalla;
            await contexto.SaveChangesAsync();
        }

        // CAMBIAR ESTADO
        public async Task CambiarEstadoAsync(int idTalla, bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);
            var talla = await contexto.Tallas.FirstOrDefaultAsync(t => t.IdTalla == idTalla);
            if (talla == null)
                throw new Exception("La talla no existe.");

            talla.Estado = estado;
            await contexto.SaveChangesAsync();
        }

        // DESACTIVAR TALLA
        public async Task DesactivarAsync(int idTalla)
        {
            await CambiarEstadoAsync(idTalla, false);
        }

        // ACTIVAR TALLA
        public async Task ActivarAsync(int idTalla)
        {
            await CambiarEstadoAsync(idTalla, true);
        }
    }
}
