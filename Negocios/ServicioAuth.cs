using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;

namespace Nk_Colletion_New.Negocios
{
    public class ServicioAuth
    {
        private readonly DbContextOptions<NkCollectionContext> _options;

        public ServicioAuth(DbContextOptions<NkCollectionContext> options)
        {
            _options = options;
        }

        public async Task<ResultadoLogin?> IniciarSesionAsync(
            string nombreUsuario,
            string contrasena)
        {
            using var context = new NkCollectionContext(_options);

            var usuario = await context.Usuarios
                .AsNoTracking()
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u =>
                    u.Usuario1 == nombreUsuario &&
                    u.Contrasena == contrasena &&
                    u.Estado == true
                );

            if (usuario == null)
                return null;

            return new ResultadoLogin
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                IdRol = usuario.IdRol,
                Rol = usuario.IdRolNavigation?.Nombre ?? "Sin rol"
            };
        }
    }
}