using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nk_Colletion_New
{
    public class Conexion_BD
    {
        string cadenaConexion =
           "Host=localhost;Port=5432;Database=NK STYLE POINT;Username=postgres;Password=131007;";

        public NpgsqlConnection ObtenerConexion()
        {
            return new NpgsqlConnection(cadenaConexion);
        }
    }
}
