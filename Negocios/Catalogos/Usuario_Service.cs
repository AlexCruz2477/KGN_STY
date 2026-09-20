using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nk_Colletion.Negocios.Seguridad;

namespace Nk_Colletion_New.Negocios.Catalogos
{
    internal class Usuario_Service
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public Usuario_Service(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        // GUARDAR USUARIO
        public async Task GuardarAsync(
            int idRol,
            string cedula,
            string nombre,
            string apellido,
            string nombreUsuario,
            string contrasena,
            string? correo)
        {
            ValidarCampos(
                idRol,
                cedula,
                nombre,
                apellido,
                nombreUsuario,
                contrasena
            );
            await using var contexto = new NkCollectionContext(_options);

            bool rolExiste = await contexto.Rols
                .AnyAsync(r => r.IdRol == idRol);

            if (!rolExiste)
                throw new Exception("El rol seleccionado no existe.");

            bool cedulaExiste = await contexto.Usuarios
                .AnyAsync(u => u.Cedula == cedula.Trim());

            if (cedulaExiste)
                throw new Exception("Ya existe un usuario con esa cédula.");

            bool usuarioExiste = await contexto.Usuarios
                .AnyAsync(u =>
                    EF.Functions.ILike(
                        u.Usuario1,
                        nombreUsuario.Trim()
                    )
                );

            if (usuarioExiste)
                throw new Exception("El nombre de usuario ya está registrado.");

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Usuarios
                    .AnyAsync(u =>
                        u.Correo != null &&
                        EF.Functions.ILike(
                            u.Correo,
                            correo.Trim()
                        )
                    );
                if (correoExiste)
                    throw new Exception("El correo electrónico ya está registrado.");
            }

            var usuario = new Usuario
            {
                IdRol = idRol,
                Cedula = cedula.Trim(),
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Usuario1 = nombreUsuario.Trim(),
                Contrasena = ContrasenaHelper.CrearHash(contrasena),
                Correo = string.IsNullOrWhiteSpace(correo)
                    ? null
                    : correo.Trim().ToLower(),
                Estado = true
            };

            contexto.Usuarios.Add(usuario);

            await contexto.SaveChangesAsync();
        }
        // LISTAR USUARIOS
        public async Task<List<Usuario>> ListarAsync()
        {
            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Usuarios
                .AsNoTracking()
                .Include(u => u.IdRolNavigation)
                .OrderBy(u => u.Nombre)
                .ThenBy(u => u.Apellido)
                .ToListAsync();
        }
        // BUSCAR USUARIOS
        public async Task<List<Usuario>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await ListarAsync();

            texto = texto.Trim();

            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Usuarios
                .AsNoTracking()
                .Include(u => u.IdRolNavigation)
                .Where(u =>
                    EF.Functions.ILike(u.Nombre, $"%{texto}%") ||
                    EF.Functions.ILike(u.Apellido, $"%{texto}%") ||
                    EF.Functions.ILike(u.Cedula, $"%{texto}%") ||
                    EF.Functions.ILike(u.Usuario1, $"%{texto}%") ||
                    (
                        u.Correo != null &&
                        EF.Functions.ILike(
                            u.Correo,
                            $"%{texto}%"
                        )
                    ) ||
                    EF.Functions.ILike(
                        u.IdRolNavigation.Nombre,
                        $"%{texto}%"
                    )
                )
                .OrderBy(u => u.Nombre)
                .ThenBy(u => u.Apellido)
                .ToListAsync();
        }
        // OBTENER USUARIO POR ID
        public async Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
        {
            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Usuarios
                .AsNoTracking()
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u =>
                    u.IdUsuario == idUsuario
                );
        }

        // EDITAR USUARIO
        public async Task EditarAsync(
            int idUsuario,
            int idRol,
            string cedula,
            string nombre,
            string apellido,
            string nombreUsuario,
            string? correo,
            string? nuevaContrasena = null)
        {
            if (idUsuario <= 0)
                throw new Exception("Usuario no válido.");

            if (idRol <= 0)
                throw new Exception("Seleccione un rol.");

            if (string.IsNullOrWhiteSpace(cedula))
                throw new Exception("Ingrese la cédula.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new Exception("Ingrese el apellido.");

            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Ingrese el nombre de usuario.");

            await using var contexto = new NkCollectionContext(_options);

            var usuario = await contexto.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.IdUsuario == idUsuario
                );

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            bool rolExiste = await contexto.Rols
                .AnyAsync(r => r.IdRol == idRol);

            if (!rolExiste)
                throw new Exception("El rol seleccionado no existe.");

            bool cedulaExiste = await contexto.Usuarios
                .AnyAsync(u =>
                    u.Cedula == cedula.Trim() &&
                    u.IdUsuario != idUsuario
                );

            if (cedulaExiste)
                throw new Exception("Ya existe otro usuario con esa cédula.");

            bool usuarioExiste = await contexto.Usuarios
                .AnyAsync(u =>
                    EF.Functions.ILike(
                        u.Usuario1,
                        nombreUsuario.Trim()
                    ) &&
                    u.IdUsuario != idUsuario
                );

            if (usuarioExiste)
                throw new Exception("Ya existe otro usuario con ese nombre.");

            if (!string.IsNullOrWhiteSpace(correo))
            {
                bool correoExiste = await contexto.Usuarios
                    .AnyAsync(u =>
                        u.Correo != null &&
                        EF.Functions.ILike(
                            u.Correo,
                            correo.Trim()
                        ) &&
                        u.IdUsuario != idUsuario
                    );

                if (correoExiste)
                    throw new Exception("Ese correo ya pertenece a otro usuario.");
            }

            usuario.IdRol = idRol;
            usuario.Cedula = cedula.Trim();
            usuario.Nombre = nombre.Trim();
            usuario.Apellido = apellido.Trim();
            usuario.Usuario1 = nombreUsuario.Trim();

            usuario.Correo = string.IsNullOrWhiteSpace(correo)
                ? null
                : correo.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(nuevaContrasena))
            {
                if (nuevaContrasena.Length < 6)
                {
                    throw new Exception(
                        "La contraseña debe tener al menos 6 caracteres."
                    );
                }

                usuario.Contrasena =
                    ContrasenaHelper.CrearHash(nuevaContrasena);
            }

            await contexto.SaveChangesAsync();
        }

        // CAMBIAR ESTADO
        public async Task CambiarEstadoAsync(
            int idUsuario,
            bool estado)
        {
            await using var contexto = new NkCollectionContext(_options);

            var usuario = await contexto.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.IdUsuario == idUsuario
                );

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            usuario.Estado = estado;

            await contexto.SaveChangesAsync();
        }
        // DESACTIVAR USUARIO
        public async Task DesactivarAsync(int idUsuario)
        {
            await CambiarEstadoAsync(
                idUsuario,
                false
            );
        }

        // ACTIVAR USUARIO
        public async Task ActivarAsync(int idUsuario)
        {
            await CambiarEstadoAsync(
                idUsuario,
                true
            );
        }
        // LISTAR ROLES
        public async Task<List<Rol>> ListarRolesAsync()
        {
            await using var contexto = new NkCollectionContext(_options);

            return await contexto.Rols
                .AsNoTracking()
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        // VALIDAR CAMPOS
        private static void ValidarCampos(
            int idRol,
            string cedula,
            string nombre,
            string apellido,
            string usuario,
            string contrasena)
        {
            if (idRol <= 0)
                throw new Exception("Seleccione un rol.");

            if (string.IsNullOrWhiteSpace(cedula))
                throw new Exception("Ingrese la cédula.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new Exception("Ingrese el apellido.");

            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Ingrese el nombre de usuario.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Ingrese una contraseña.");

            if (contrasena.Length < 6)
            {
                throw new Exception(
                    "La contraseña debe tener al menos 6 caracteres."
                );
            }
        }
    }

}

