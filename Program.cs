using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Presentacion.Autenticacion;

namespace Nk_Colletion_New;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var options = new DbContextOptionsBuilder<NkCollectionContext>()
            .UseNpgsql(AppConfig.CadenaConexion)
            .Options;

        AppConfig.DbOptions = options;

        var servicioAuth = new ServicioAuth(options);
        var loginServicio = new LoginServicio(options);

        Application.Run(new Frm_Login(servicioAuth, loginServicio));
    }
}
