using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using ColorModel = Nk_Colletion_New.Datos.Modelos.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Servicios.Productos
{
    internal class Color_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Color_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR COLOR
        public async Task GuardarAsync(string nombreColor)
        {
            if (string.IsNullOrWhiteSpace(nombreColor))
                throw new Exception("Ingrese el nombre del color.");

            nombreColor = nombreColor.Trim();

            await using var contexto = new NkCollectionContext(_options);
            bool existe = await contexto.Colors.AnyAsync(c => EF.Functions.ILike(c.NombreColor, nombreColor));
            if (existe)
                throw new Exception("El color ya está registrado.");

            var color = new ColorModel
            {
                NombreColor = nombreColor,
                Estado = true
            };

            contexto.Colors.Add(color);
            await contexto.SaveChangesAsync();
        }

        // LISTAR COLORES
        public async Task<List<ColorModel>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Colors.AsNoTracking().OrderBy(c => c.NombreColor).ToListAsync();
        }

        // BUSCAR COLORES
        public async Task<List<ColorModel>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Colors.AsNoTracking()
                .Where(c => EF.Functions.ILike(c.NombreColor, $"%{texto}%"))
                .OrderBy(c => c.NombreColor)
                .ToListAsync();
        }

        // OBTENER COLOR POR ID
        public async Task<ColorModel?> ObtenerPorIdAsync(int idColor)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Colors.AsNoTracking().FirstOrDefaultAsync(c => c.IdColor == idColor);
        }

        // EDITAR COLOR
        public async Task EditarAsync(int idColor, string nombreColor)
        {
            if (idColor <= 0)
                throw new Exception("Color no válido.");

            if (string.IsNullOrWhiteSpace(nombreColor))
                throw new Exception("Ingrese el nombre del color.");

            nombreColor = nombreColor.Trim();

            await using var contexto = new NkCollectionContext(_options);
            var color = await contexto.Colors.FirstOrDefaultAsync(c => c.IdColor == idColor);
            if (color == null)
                throw new Exception("El color no existe.");

            bool existe = await contexto.Colors.AnyAsync(c => EF.Functions.ILike(c.NombreColor, nombreColor) && c.IdColor != idColor);
            if (existe)
                throw new Exception("Ya existe otro color con ese nombre.");

            color.NombreColor = nombreColor;
            await contexto.SaveChangesAsync();
        }

        // CAMBIAR ESTADO
        public async Task CambiarEstadoAsync(int idColor, bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);
            var color = await contexto.Colors.FirstOrDefaultAsync(c => c.IdColor == idColor);
            if (color == null)
                throw new Exception("El color no existe.");

            color.Estado = estado;
            await contexto.SaveChangesAsync();
        }

        // DESACTIVAR COLOR
        public async Task DesactivarAsync(int idColor)
        {
            await CambiarEstadoAsync(idColor, false);
        }

        // ACTIVAR COLOR
        public async Task ActivarAsync(int idColor)
        {
            await CambiarEstadoAsync(idColor, true);
        }
    }
}

