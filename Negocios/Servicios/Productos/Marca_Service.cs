using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Servicios.Productos
{
    internal class Marca_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Marca_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR MARCA
        public async Task GuardarAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre de la marca.");

            nombre = nombre.Trim();

            await using var contexto = new NkCollectionContext(_options);
            bool existe = await contexto.Marcas.AnyAsync(m => EF.Functions.ILike(m.Nombre, nombre));
            if (existe)
                throw new Exception("La marca ya está registrada.");

            var marca = new Marca
            {
                Nombre = nombre,
                Estado = true
            };

            contexto.Marcas.Add(marca);
            await contexto.SaveChangesAsync();
        }

        // LISTAR MARCAS
        public async Task<List<Marca>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Marcas.AsNoTracking().OrderBy(m => m.Nombre).ToListAsync();
        }

        // BUSCAR MARCAS
        public async Task<List<Marca>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Marcas.AsNoTracking()
                .Where(m => EF.Functions.ILike(m.Nombre, $"%{texto}%"))
                .OrderBy(m => m.Nombre)
                .ToListAsync();
        }

        // OBTENER MARCA POR ID
        public async Task<Marca?> ObtenerPorIdAsync(int idMarca)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Marcas.AsNoTracking().FirstOrDefaultAsync(m => m.IdMarca == idMarca);
        }

        // EDITAR MARCA
        public async Task EditarAsync(int idMarca, string nombre)
        {
            if (idMarca <= 0)
                throw new Exception("Marca no válida.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre de la marca.");

            nombre = nombre.Trim();

            await using var contexto = new NkCollectionContext(_options);
            var marca = await contexto.Marcas.FirstOrDefaultAsync(m => m.IdMarca == idMarca);
            if (marca == null)
                throw new Exception("La marca no existe.");

            bool existe = await contexto.Marcas.AnyAsync(m => EF.Functions.ILike(m.Nombre, nombre) && m.IdMarca != idMarca);
            if (existe)
                throw new Exception("Ya existe otra marca con ese nombre.");

            marca.Nombre = nombre;
            await contexto.SaveChangesAsync();
        }

        // CAMBIAR ESTADO
        public async Task CambiarEstadoAsync(int idMarca, bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);
            var marca = await contexto.Marcas.FirstOrDefaultAsync(m => m.IdMarca == idMarca);
            if (marca == null)
                throw new Exception("La marca no existe.");

            marca.Estado = estado;
            await contexto.SaveChangesAsync();
        }

        // DESACTIVAR MARCA
        public async Task DesactivarAsync(int idMarca)
        {
            await CambiarEstadoAsync(idMarca, false);
        }

        // ACTIVAR MARCA
        public async Task ActivarAsync(int idMarca)
        {
            await CambiarEstadoAsync(idMarca, true);
        }
    }
}
