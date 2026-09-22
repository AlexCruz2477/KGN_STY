using Microsoft.EntityFrameworkCore;
using Nk_Colletion.Negocios.Seguridad;
using Nk_Colletion_New.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace Nk_Colletion_New.Negocios.Autenticacion
{
    public class LoginServicio
    {
        private readonly DbContextOptions<NkCollectionContext> _opciones;

        public LoginServicio(DbContextOptions<NkCollectionContext> opciones)
        {
            _opciones = opciones;
        }

        public bool EnviarCodigoRecuperacion(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            var correoNormalizado = correo.Trim();
            // Mantener la lógica original: token como GUID (cadena única)
            var codigo = Guid.NewGuid().ToString();

            using var contexto = new NkCollectionContext(_opciones);
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.Correo == correoNormalizado && u.Estado);

            if (usuario == null)
                return false;

            usuario.TokenRecuperacion = codigo;
            usuario.FechaHoraRecuperacion = DateTime.Now.AddMinutes(15);
            contexto.Usuarios.Update(usuario);
            contexto.SaveChanges();

            // Enviar el correo. Si falla, revertir el token y devolver false.
            if (EnviarCorreo(correoNormalizado, codigo))
            {
                usuario.TokenRecuperacion = null;
                usuario.FechaHoraRecuperacion = null;
                contexto.Usuarios.Update(usuario);
                contexto.SaveChanges();
                return false;
            }

            return true;
        }

        public bool EnviarCorreo(string correoDestino, string codigo)
        {
            try
            {
                string correoZoho = "nk_collection@gmail.com";
                string contrasenaZoho = "MG4ihpbHWj7H";

                using var smtp = new SmtpClient("smtp.zoho.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(correoZoho, contrasenaZoho),
                    EnableSsl = true
                };

                string asuntoCorreo = "NK Style point - recuperacion de contraseña";
                string cuerpoCorreo = $@"
<html>
<body>
    <h2 style='color:#800000;'>NK Style Point -Recuperación de contraseña</h2>
    <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</p>
    <p><b>Tu código de recuperación es:</b></p>
    <h3 style='color:#d35400;'>{WebUtility.HtmlEncode(codigo)}</h3>
    <p>Este código es válido por 15 minutos.</p>
    <p>Si no solicitaste este cambio, puedes ignorar este mensaje.</p>
</body>
</html>";
                   using var mensajeCorreo = new MailMessage(correoZoho, correoDestino)
                   {
                        From = new MailAddress(correoZoho),
                        Subject = asuntoCorreo,
                        Body = cuerpoCorreo,
                        IsBodyHtml = true
                   };

                mensajeCorreo.To.Add(correoDestino);
                smtp.Send(mensajeCorreo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
