using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using System.Security.Cryptography;

namespace Nk_Colletion_New.Negocios.Autenticacion;

public class AutenticacionUsuario
{
    private readonly DbContextOptions<NkCollectionContext> _opciones;

    public AutenticacionUsuario(DbContextOptions<NkCollectionContext> opciones)
    {
        _opciones = opciones;
    }

    public Usuario? ValidarCredenciales(string usuario, string contraseña)
    {
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            return null;

        using var contexto = new NkCollectionContext(_opciones);

        var usuarioEncontrado = contexto.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefault(u => u.Usuario1 == usuario.Trim() && u.Estado);

        if (usuarioEncontrado is null || !VerificarContraseña(contraseña, usuarioEncontrado.Contrasena))
            return null;

        if (!EsFormatoActual(usuarioEncontrado.Contrasena))
        {
            usuarioEncontrado.Contrasena = GenerarHash(contraseña);
            contexto.Usuarios.Update(usuarioEncontrado);
            contexto.SaveChanges();
        }

        return usuarioEncontrado;
    }

    private static string GenerarHash(string contraseña)
    {
        const int iteraciones = 100_000;
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            contraseña,
            salt,
            iteraciones,
            HashAlgorithmName.SHA256,
            32);

        return $"PBKDF2$SHA256${iteraciones}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    private static bool VerificarContraseña(string contraseña, string valorGuardado)
    {
        if (!valorGuardado.StartsWith("PBKDF2$", StringComparison.Ordinal))
            return string.Equals(contraseña, valorGuardado, StringComparison.Ordinal);

        string[] partes = valorGuardado.Split('$');
        int indiceIteraciones;
        int indiceSalt;
        int indiceHash;

        if (partes.Length == 4)
        {
            // Formato existente en la base de datos: PBKDF2$iteraciones$salt$hash
            indiceIteraciones = 1;
            indiceSalt = 2;
            indiceHash = 3;
        }
        else if (partes.Length == 5 &&
                 string.Equals(partes[1], "SHA256", StringComparison.OrdinalIgnoreCase))
        {
            // Formato generado por esta aplicación: PBKDF2$SHA256$iteraciones$salt$hash
            indiceIteraciones = 2;
            indiceSalt = 3;
            indiceHash = 4;
        }
        else
        {
            return false;
        }

        if (!int.TryParse(partes[indiceIteraciones], out int iteraciones))
        {
            return false;
        }

        try
        {
            byte[] salt = Convert.FromBase64String(partes[indiceSalt]);
            byte[] hashEsperado = Convert.FromBase64String(partes[indiceHash]);
            byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                contraseña,
                salt,
                iteraciones,
                HashAlgorithmName.SHA256,
                hashEsperado.Length);

            return CryptographicOperations.FixedTimeEquals(hashCalculado, hashEsperado);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static bool EsFormatoActual(string valorGuardado)
    {
        string[] partes = valorGuardado.Split('$');
        return partes.Length == 5 &&
               partes[0] == "PBKDF2" &&
               string.Equals(partes[1], "SHA256", StringComparison.OrdinalIgnoreCase);
    }
}