using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Catalogos
{
    internal class Proveedor_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Proveedor_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        public async Task GuardarAsync(
            string nombre,
            string? telefono,
            string? correo,
            string? direccion,
            string? ruc)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre del proveedor.");

            await using var contexto = new NkCollectionContext(_options);

            if (!string.IsNullOrWhiteSpace(ruc))
            {
                bool rucExiste = await contexto.Proveedors
                    .AnyAsync(p => p.Ruc != null && p.Ruc == ruc.Trim());
                if (rucExiste)
                    throw new Exception("Ya existe un proveedor con ese RUC.");
            }

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Proveedors
                    .AnyAsync(p => p.Correo != null && EF.Functions.ILike(p.Correo, correo.Trim()));
                if (correoExiste)
                    throw new Exception("El correo electrónico ya está registrado para otro proveedor.");
            }

            var proveedor = new Proveedor
            {
                Nombre = nombre.Trim(),
                Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim(),
                Correo = string.IsNullOrWhiteSpace(correo) ? null : correo.Trim().ToLower(),
                Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
                Ruc = string.IsNullOrWhiteSpace(ruc) ? null : ruc.Trim(),
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            contexto.Proveedors.Add(proveedor);
            await contexto.SaveChangesAsync();
        }

        public async Task<List<Proveedor>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Proveedors
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<List<Proveedor>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Proveedors
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.ILike(p.Nombre, $"%{texto}%") ||
                    (p.Ruc != null && EF.Functions.ILike(p.Ruc, $"%{texto}%")) ||
                    (p.Correo != null && EF.Functions.ILike(p.Correo, $"%{texto}%")) ||
                    (p.Telefono != null && EF.Functions.ILike(p.Telefono, $"%{texto}%"))
                )
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<Proveedor?> ObtenerPorIdAsync(int idProveedor)
        {
            await using var contexto = new NkCollectionContext(_options);
            return await contexto.Proveedors
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdProveedor == idProveedor);
        }

        public async Task EditarAsync(
            int idProveedor,
            string nombre,
            string? telefono,
            string? correo,
            string? direccion,
            string? ruc,
            bool? estado)
        {
            if (idProveedor <= 0)
                throw new Exception("Proveedor no válido.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre del proveedor.");

            await using var contexto = new NkCollectionContext(_options);

            var proveedor = await contexto.Proveedors
                .FirstOrDefaultAsync(p => p.IdProveedor == idProveedor);

            if (proveedor == null)
                throw new Exception("El proveedor no existe.");

            if (!string.IsNullOrWhiteSpace(ruc))
            {
                bool rucExiste = await contexto.Proveedors
                    .AnyAsync(p => p.Ruc != null && p.Ruc == ruc.Trim() && p.IdProveedor != idProveedor);
                if (rucExiste)
                    throw new Exception("Ya existe otro proveedor con ese RUC.");
            }

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Proveedors
                    .AnyAsync(p => p.Correo != null && EF.Functions.ILike(p.Correo, correo.Trim()) && p.IdProveedor != idProveedor);
                if (correoExiste)
                    throw new Exception("Ese correo ya pertenece a otro proveedor.");
            }

            proveedor.Nombre = nombre.Trim();
            proveedor.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            proveedor.Correo = string.IsNullOrWhiteSpace(correo) ? null : correo.Trim().ToLower();
            proveedor.Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim();
            proveedor.Ruc = string.IsNullOrWhiteSpace(ruc) ? null : ruc.Trim();
            proveedor.Estado = estado ?? proveedor.Estado;

            await contexto.SaveChangesAsync();
        }
    }
}
