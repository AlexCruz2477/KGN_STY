using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios;
using Nk_Colletion_New.Presentacion.Autenticacion;
using System.Windows.Forms;
using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var options = new DbContextOptionsBuilder<NkCollectionContext>()
                .UseNpgsql(
                    "Host=localhost;" +
                    "Port=5432;" +
                    "Database=NK_STYLE_POINT;" +
                    "Username=postgres;" +
                    "Password=131007"
                )
                .Options;

            var autenticacionUsuario = new AutenticacionUsuario(options);

            Application.Run(
                new Frm_Login(autenticacionUsuario)
            );
        }
    }
}