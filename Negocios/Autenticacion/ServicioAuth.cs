using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Seguridad;

namespace Nk_Colletion_New.Negocios.Autenticacion;

public sealed class ServicioAuth
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    public ServicioAuth(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<UsuarioSesion?> ValidarCredencialesAsync(
        string nombreUsuario,
        string contrasena)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario) ||
            string.IsNullOrWhiteSpace(contrasena))
        {
            return null;
        }

        await using var contexto = new NkCollectionContext(_options);

        var usuario = await contexto.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u =>
                EF.Functions.ILike(u.Usuario1, nombreUsuario.Trim()));

        if (usuario is null || !usuario.Estado)
        {
            return null;
        }

        bool contrasenaCorrecta;
        if (ContrasenaHelper.EsHashPbkdf2(usuario.Contrasena))
        {
            contrasenaCorrecta = ContrasenaHelper.Verificar(
                contrasena,
                usuario.Contrasena);
        }
        else
        {
            contrasenaCorrecta = usuario.Contrasena == contrasena;

            if (contrasenaCorrecta)
            {
                usuario.Contrasena = ContrasenaHelper.CrearHash(contrasena);
                await contexto.SaveChangesAsync();
            }
        }

        if (!contrasenaCorrecta)
        {
            return null;
        }

        return new UsuarioSesion
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}".Trim(),
            NombreUsuario = usuario.Usuario1,
            Correo = usuario.Correo ?? string.Empty,
            Rol = usuario.IdRolNavigation?.Nombre ?? "Sin rol"
        };
    }
}

public sealed class UsuarioSesion
{
    public int IdUsuario { get; init; }
    public int IdRol { get; init; }
    public string NombreCompleto { get; init; } = string.Empty;
    public string NombreUsuario { get; init; } = string.Empty;
    public string Correo { get; init; } = string.Empty;
    public string Rol { get; init; } = string.Empty;
}
