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
    internal class Cliente_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Cliente_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR CLIENTE
        public async Task GuardarAsync(
            string? cedula,
            string nombre,
            string apellido,
            string? telefono,
            string? correo,
            string? direccion)
        {
            ValidarCamposGuardar(nombre, apellido);

            await using var contexto = new NkCollectionContext(_options);

            if (!string.IsNullOrWhiteSpace(cedula))
            {
                bool cedulaExiste = await contexto.Clientes
                    .AnyAsync(c => c.Cedula != null && c.Cedula == cedula.Trim());

                if (cedulaExiste)
                    throw new Exception("Ya existe un cliente con esa cédula.");
            }

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Clientes
                    .AnyAsync(c => c.Correo != null && EF.Functions.ILike(c.Correo, correo.Trim()));
                if (correoExiste)
                    throw new Exception("El correo electrónico ya está registrado.");
            }

            var cliente = new Cliente
            {
                Cedula = string.IsNullOrWhiteSpace(cedula) ? null : cedula.Trim(),
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim(),
                Correo = string.IsNullOrWhiteSpace(correo) ? null : correo.Trim().ToLower(),
                Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            contexto.Clientes.Add(cliente);
            await contexto.SaveChangesAsync();
        }

        // LISTAR CLIENTES
        public async Task<List<Cliente>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Nombre)
                .ThenBy(c => c.Apellido)
                .ToListAsync();
        }

        // BUSCAR CLIENTES
        public async Task<List<Cliente>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Clientes
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.ILike(c.Nombre, $"%{texto}%") ||
                    EF.Functions.ILike(c.Apellido, $"%{texto}%") ||
                    (c.Cedula != null && EF.Functions.ILike(c.Cedula, $"%{texto}%")) ||
                    (c.Correo != null && EF.Functions.ILike(c.Correo, $"%{texto}%")) ||
                    (c.Telefono != null && EF.Functions.ILike(c.Telefono, $"%{texto}%"))
                )
                .OrderBy(c => c.Nombre)
                .ThenBy(c => c.Apellido)
                .ToListAsync();
        }

        // OBTENER CLIENTE POR ID
        public async Task<Cliente?> ObtenerPorIdAsync(int idCliente)
        {
            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente);
        }

        // EDITAR CLIENTE
        public async Task EditarAsync(
            int idCliente,
            string? cedula,
            string nombre,
            string apellido,
            string? telefono,
            string? correo,
            string? direccion,
            bool? estado)
        {
            if (idCliente <= 0)
                throw new Exception("Cliente no válido.");

            ValidarCamposGuardar(nombre, apellido);

            await using var contexto = new NkCollectionContext(_options);

            var cliente = await contexto.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente);

            if (cliente == null)
                throw new Exception("El cliente no existe.");

            if (!string.IsNullOrWhiteSpace(cedula))
            {
                bool cedulaExiste = await contexto.Clientes
                    .AnyAsync(c => c.Cedula != null && c.Cedula == cedula.Trim() && c.IdCliente != idCliente);
                if (cedulaExiste)
                    throw new Exception("Ya existe otro cliente con esa cédula.");
            }

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Clientes
                    .AnyAsync(c => c.Correo != null && EF.Functions.ILike(c.Correo, correo.Trim()) && c.IdCliente != idCliente);
                if (correoExiste)
                    throw new Exception("Ese correo ya pertenece a otro cliente.");
            }

            cliente.Cedula = string.IsNullOrWhiteSpace(cedula) ? null : cedula.Trim();
            cliente.Nombre = nombre.Trim();
            cliente.Apellido = apellido.Trim();
            cliente.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            cliente.Correo = string.IsNullOrWhiteSpace(correo) ? null : correo.Trim().ToLower();
            cliente.Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim();
            cliente.Estado = estado ?? cliente.Estado;

            await contexto.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync(int idCliente, bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);
            var cliente = await contexto.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente)
                ?? throw new Exception("El cliente no existe.");

            cliente.Estado = estado;
            await contexto.SaveChangesAsync();
        }

        public Task DesactivarAsync(int idCliente) =>
            CambiarEstadoAsync(idCliente, false);

        public Task ActivarAsync(int idCliente) =>
            CambiarEstadoAsync(idCliente, true);

        private static void ValidarCamposGuardar(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new Exception("Ingrese el apellido.");
        }
    }
}
