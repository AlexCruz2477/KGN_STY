using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios;
using Nk_Colletion_New.Presentacion.Autenticacion;

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

            var servicioAuth = new ServicioAuth(options);

            Application.Run(
                new Frm_Login(servicioAuth)
            );
        }
    }
}