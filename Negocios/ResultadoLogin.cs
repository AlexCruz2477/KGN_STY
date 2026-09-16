namespace Nk_Colletion_New.Negocios
{
    public class ResultadoLogin
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public int? IdRol { get; set; }

        public string Rol { get; set; } = string.Empty;
    }
}