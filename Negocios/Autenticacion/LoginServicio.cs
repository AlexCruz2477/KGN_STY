using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Seguridad;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Nk_Colletion_New.Negocios.Autenticacion;

public sealed class LoginServicio
{
    private readonly DbContextOptions<NkCollectionContext> _options;

    private static readonly string CorreoRemitente =
        Environment.GetEnvironmentVariable("NK_SMTP_USER")
        ?? "nk_collection@zohomail.com";

    private static readonly string ContrasenaCorreo =
        Environment.GetEnvironmentVariable("NK_SMTP_PASSWORD")
        ?? "MG4ihpbHWj7H";

    private static readonly string ServidorSmtp =
        Environment.GetEnvironmentVariable("NK_SMTP_HOST")
        ?? "smtp.zoho.com";

    private static readonly int PuertoSmtp =
        int.TryParse(Environment.GetEnvironmentVariable("NK_SMTP_PORT"), out int puerto)
            ? puerto
            : 587;

    public LoginServicio(DbContextOptions<NkCollectionContext> options)
    {
        _options = options;
    }

    public async Task<bool> EnviarCodigoRecuperacionAsync(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            return false;
        }

        correo = correo.Trim().ToLowerInvariant();
        if (!MailAddress.TryCreate(correo, out _))
        {
            throw new ArgumentException("El correo electrónico no es válido.");
        }

        await using var contexto = new NkCollectionContext(_options);
        var usuario = await contexto.Usuarios
            .FirstOrDefaultAsync(u =>
                u.Correo != null &&
                EF.Functions.ILike(u.Correo, correo) &&
                u.Estado);

        if (usuario is null)
        {
            return false;
        }

        string codigo = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        usuario.TokenRecuperacion = CrearHashCodigo(codigo);
        usuario.FechaHoraRecuperacion = DateTime.Now.AddMinutes(15);
        await contexto.SaveChangesAsync();

        try
        {
            await EnviarCorreoAsync(usuario.Correo!, usuario.Nombre, codigo);
            return true;
        }
        catch
        {
            usuario.TokenRecuperacion = null;
            usuario.FechaHoraRecuperacion = null;
            await contexto.SaveChangesAsync();
            throw;
        }
    }

    public async Task<bool> ValidarCodigoAsync(string correo, string codigo)
    {
        if (string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(codigo))
        {
            return false;
        }

        correo = correo.Trim().ToLowerInvariant();
        codigo = codigo.Trim();

        await using var contexto = new NkCollectionContext(_options);
        var usuario = await contexto.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.Correo != null &&
                EF.Functions.ILike(u.Correo, correo) &&
                u.Estado);

        if (usuario is null ||
            string.IsNullOrWhiteSpace(usuario.TokenRecuperacion) ||
            !usuario.FechaHoraRecuperacion.HasValue ||
            usuario.FechaHoraRecuperacion.Value <= DateTime.Now)
        {
            return false;
        }

        return VerificarCodigo(codigo, usuario.TokenRecuperacion);
    }

    public async Task<bool> CambiarContrasenaAsync(
        string correo,
        string codigo,
        string nuevaContrasena)
    {
        if (string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(codigo) ||
            string.IsNullOrWhiteSpace(nuevaContrasena))
        {
            return false;
        }

        if (nuevaContrasena.Length < 6)
        {
            throw new ArgumentException(
                "La contraseña debe tener al menos 6 caracteres.");
        }

        correo = correo.Trim().ToLowerInvariant();
        codigo = codigo.Trim();

        await using var contexto = new NkCollectionContext(_options);
        var usuario = await contexto.Usuarios
            .FirstOrDefaultAsync(u =>
                u.Correo != null &&
                EF.Functions.ILike(u.Correo, correo) &&
                u.Estado);

        if (usuario is null ||
            string.IsNullOrWhiteSpace(usuario.TokenRecuperacion) ||
            !usuario.FechaHoraRecuperacion.HasValue)
        {
            return false;
        }

        if (usuario.FechaHoraRecuperacion.Value <= DateTime.Now)
        {
            usuario.TokenRecuperacion = null;
            usuario.FechaHoraRecuperacion = null;
            await contexto.SaveChangesAsync();
            return false;
        }

        if (!VerificarCodigo(codigo, usuario.TokenRecuperacion))
        {
            return false;
        }

        usuario.Contrasena = ContrasenaHelper.CrearHash(nuevaContrasena);
        usuario.TokenRecuperacion = null;
        usuario.FechaHoraRecuperacion = null;
        await contexto.SaveChangesAsync();
        return true;
    }

    private static string CrearHashCodigo(string codigo)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(codigo));
        return Convert.ToHexString(hash);
    }

    private static bool VerificarCodigo(string codigo, string hashGuardado)
    {
        try
        {
            byte[] codigoHash = SHA256.HashData(Encoding.UTF8.GetBytes(codigo));
            byte[] hashEsperado = Convert.FromHexString(hashGuardado);
            return CryptographicOperations.FixedTimeEquals(codigoHash, hashEsperado);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static async Task EnviarCorreoAsync(
        string correoDestino,
        string nombre,
        string codigo)
    {
        using var smtp = new SmtpClient(ServidorSmtp, PuertoSmtp)
        {
            Credentials = new NetworkCredential(CorreoRemitente, ContrasenaCorreo),
            EnableSsl = true
        };

        using var mensaje = new MailMessage
        {
            From = new MailAddress(CorreoRemitente, "NK Style Point"),
            Subject = "NK Style Point - Recuperación de contraseña",
            IsBodyHtml = true,
            Body = $@"
<html>
<body style='font-family:Arial,sans-serif;background:#f5f5f5;padding:30px;'>
<div style='max-width:600px;margin:auto;background:white;padding:35px;border-radius:12px;'>
    <h2 style='color:#6E1220;'>Recuperación de contraseña</h2>
    <p>Hola <b>{WebUtility.HtmlEncode(nombre)}</b>,</p>
    <p>Tu código de recuperación es:</p>
    <div style='text-align:center;font-size:32px;font-weight:bold;letter-spacing:8px;color:#6E1220;'>
        {codigo}
    </div>
    <p>Este código será válido durante <b>15 minutos</b>.</p>
    <p>Si no solicitaste este cambio, puedes ignorar este mensaje.</p>
</div>
</body>
</html>"
        };

        mensaje.To.Add(correoDestino);

        try
        {
            await smtp.SendMailAsync(mensaje);
        }
        catch (SmtpException ex)
        {
            throw new InvalidOperationException(
                "No se pudo enviar el correo de recuperación. " + ex.Message,
                ex);
        }
    }
}
