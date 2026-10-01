using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos
{
    // Contenedor estático para opciones de DbContext compartidas en la aplicación.
    public static class AppConfig
    {
        public static DbContextOptions<NkCollectionContext>? DbOptions { get; set; }
    }
}
