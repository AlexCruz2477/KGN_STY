using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Servicios.Productos
{
    internal class Categoria_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Categoria_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR CATEGORIA
        public async Task GuardarAsync(string nombreCategoria, string? descripcion = null)
        {
            if (string.IsNullOrWhiteSpace(nombreCategoria))
                throw new Exception("Ingrese el nombre de la categoría.");

            nombreCategoria = nombreCategoria.Trim();

            await using var contexto = new NkCollectionContext(_options);
            bool existe = await contexto.Categoria.AnyAsync(c => EF.Functions.ILike(c.NombreCategoria, nombreCategoria));
            if (existe)
                throw new Exception("La categoría ya está registrada.");

            var categoria = new Categorium
            {
                NombreCategoria = nombreCategoria,
                Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim(),
                Estado = true
            };

            contexto.Categoria.Add(categoria);
            await contexto.SaveChangesAsync();
        }

        // LISTAR CATEGORIAS
        public async Task<List<Categorium>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Categoria.AsNoTracking().OrderBy(c => c.NombreCategoria).ToListAsync();
        }

        // BUSCAR CATEGORIAS
        public async Task<List<Categorium>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Categoria.AsNoTracking()
                .Where(c => EF.Functions.ILike(c.NombreCategoria, $"%{texto}%") || EF.Functions.ILike(c.Descripcion ?? string.Empty, $"%{texto}%"))
                .OrderBy(c => c.NombreCategoria)
                .ToListAsync();
        }

        // OBTENER CATEGORIA POR ID
        public async Task<Categorium?> ObtenerPorIdAsync(int idCategoria)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Categoria.AsNoTracking().FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
        }

        // EDITAR CATEGORIA
        public async Task EditarAsync(int idCategoria, string nombreCategoria, string? descripcion = null)
        {
            if (idCategoria <= 0)
                throw new Exception("Categoria no válida.");

            if (string.IsNullOrWhiteSpace(nombreCategoria))
                throw new Exception("Ingrese el nombre de la categoría.");

            nombreCategoria = nombreCategoria.Trim();

            await using var contexto = new NkCollectionContext(_options);
            var categoria = await contexto.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
            if (categoria == null)
                throw new Exception("La categoría no existe.");

            bool existe = await contexto.Categoria.AnyAsync(c => EF.Functions.ILike(c.NombreCategoria, nombreCategoria) && c.IdCategoria != idCategoria);
            if (existe)
                throw new Exception("Ya existe otra categoría con ese nombre.");

            categoria.NombreCategoria = nombreCategoria;
            categoria.Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim();
            await contexto.SaveChangesAsync();
        }

        // CAMBIAR ESTADO
        public async Task CambiarEstadoAsync(int idCategoria, bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);
            var categoria = await contexto.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
            if (categoria == null)
                throw new Exception("La categoría no existe.");

            categoria.Estado = estado;
            await contexto.SaveChangesAsync();
        }

        // DESACTIVAR CATEGORIA
        public async Task DesactivarAsync(int idCategoria)
        {
            await CambiarEstadoAsync(idCategoria, false);
        }

        // ACTIVAR CATEGORIA
        public async Task ActivarAsync(int idCategoria)
        {
            await CambiarEstadoAsync(idCategoria, true);
        }
    }
}

