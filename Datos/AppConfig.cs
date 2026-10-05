using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos;

public static class AppConfig
{
    public static DbContextOptions<NkCollectionContext>? DbOptions { get; set; }

    public static string CadenaConexion =>
        "Host=localhost;" +
        "Port=5432;" +
        "Database=NK_STYLE_POINT;" +
        "Username=postgres;" +
        "Password=131007";

    public static DbContextOptions<NkCollectionContext> ObtenerOpciones()
    {
        return DbOptions
            ?? throw new InvalidOperationException(
                "La conexion a la base de datos todavia no ha sido inicializada.");
    }
}
