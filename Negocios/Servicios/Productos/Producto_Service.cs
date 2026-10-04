using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Negocios.Servicios.Productos
{
    public class Producto_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Producto_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        //listar productos
        public async Task<List<Producto>> ListarProductosAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Productos.AsNoTracking()
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.ProductoVariantes)
                .OrderBy(p => p.IdProducto)
                .ToListAsync();
        }

        //listar variantes
        public async Task<List<ProductoVariante>> ListarVariantesAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.ProductoVariantes.AsNoTracking()
                .Include(v => v.IdProductoNavigation).ThenInclude(p => p.IdCategoriaNavigation)
                .Include(v => v.IdProductoNavigation).ThenInclude(p => p.IdMarcaNavigation)
                .Include(v => v.IdTallaNavigation)
                .Include(v => v.IdColorNavigation)
                .OrderBy(v => v.IdProducto)
                .ThenBy(v => v.IdVariante)
                .ToListAsync();
        }

        //obtener producto
        public async Task<Producto?> ObtenerPorIdAsync(int idProducto)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Productos.AsNoTracking()
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto);
        }

        //listar variantes del producto
        public async Task<List<ProductoVariante>> ObtenerVariantesPorProductoAsync(int idProducto)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.ProductoVariantes.AsNoTracking()
                .Include(v => v.IdTallaNavigation)
                .Include(v => v.IdColorNavigation)
                .Where(v => v.IdProducto == idProducto)
                .OrderBy(v => v.IdVariante)
                .ToListAsync();
        }

        //validar variante
        private static void ValidarVariante(
            int? idTalla,
            int? idColor,
            int stockActual,
            int stockMinimo,
            decimal precioCompra,
            decimal precioVenta)
        {
            if (idTalla is null)
            {
                throw new Exception("Seleccione una talla.");
            }

            if (idColor is null)
            {
                throw new Exception("Seleccione un color.");
            }

            if (stockActual < 0 || stockMinimo < 0)
            {
                throw new Exception("El stock no puede ser negativo.");
            }

            if (precioCompra < 0 || precioVenta < 0)
            {
                throw new Exception("Los precios no pueden ser negativos.");
            }
        }

        //agregar variante
        public async Task<int> AgregarVarianteAsync(
            int idProducto,
            int? idTalla,
            int? idColor,
            int stockActual,
            int stockMinimo,
            decimal precioCompra,
            decimal precioVenta)
        {
            ValidarVariante(idTalla, idColor, stockActual, stockMinimo, precioCompra, precioVenta);

            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();

            var producto = await contexto.Productos.FirstOrDefaultAsync(p => p.IdProducto == idProducto);
            if (producto is null)
            {
                throw new Exception("El producto seleccionado no existe.");
            }

            if (producto.Estado == false)
            {
                throw new Exception("No se pueden agregar variantes a un producto inactivo.");
            }

            bool existe = await contexto.ProductoVariantes.AnyAsync(v => v.IdProducto == idProducto && v.IdTalla == idTalla && v.IdColor == idColor && v.Estado != false);
            if (existe)
            {
                throw new Exception("Ese producto ya tiene una variante con la misma talla y color.");
            }

            var variante = new ProductoVariante
            {
                IdProducto = idProducto,
                IdTalla = idTalla,
                IdColor = idColor,
                Codigo = $"TMP-{Guid.NewGuid():N}",
                StockActual = stockActual,
                StockMinimo = stockMinimo,
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                Estado = true
            };

            contexto.ProductoVariantes.Add(variante);
            await contexto.SaveChangesAsync();

            variante.Codigo = $"NK-P{producto.IdProducto:D6}-V{variante.IdVariante:D3}";
            await contexto.SaveChangesAsync();

            await transaccion.CommitAsync();
            return variante.IdVariante;
        }

        //guardar producto + variante
        public async Task<int> GuardarAsync(
            string nombre,
            string? descripcion,
            int? idCategoria,
            int? idMarca,
            int? idTalla,
            int? idColor,
            int stockActual,
            int stockMinimo,
            decimal precioCompra,
            decimal precioVenta)
        {
            nombre = nombre.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception("Ingrese el nombre del producto.");
            }

            if (idCategoria is null)
            {
                throw new Exception("Seleccione una categoría.");
            }

            if (idMarca is null)
            {
                throw new Exception("Seleccione una marca.");
            }

            ValidarVariante(idTalla, idColor, stockActual, stockMinimo, precioCompra, precioVenta);

            await using var contexto = new NkCollectionContext(_options);
            await using var transaccion = await contexto.Database.BeginTransactionAsync();

            var producto = new Producto
            {
                NombreProducto = nombre,
                Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim(),
                IdCategoria = idCategoria,
                IdMarca = idMarca,
                Estado = true
            };

            contexto.Productos.Add(producto);
            await contexto.SaveChangesAsync();

            var variante = new ProductoVariante
            {
                IdProducto = producto.IdProducto,
                IdTalla = idTalla,
                IdColor = idColor,
                Codigo = $"TMP-{Guid.NewGuid():N}",
                StockActual = stockActual,
                StockMinimo = stockMinimo,
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                Estado = true
            };

            contexto.ProductoVariantes.Add(variante);
            await contexto.SaveChangesAsync();

            variante.Codigo = $"NK-P{producto.IdProducto:D6}-V{variante.IdVariante:D3}";
            await contexto.SaveChangesAsync();

            await transaccion.CommitAsync();
            return producto.IdProducto;
        }
    }
}
