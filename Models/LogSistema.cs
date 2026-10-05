using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class LogSistema
{
    public long IdLog { get; set; }

    public DateTime Fecha { get; set; }

    public string Modulo { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public string TablaAfectada { get; set; } = null!;

    public string? IdRegistro { get; set; }

    public string? Descripcion { get; set; }

    public string? DatosAnteriores { get; set; }

    public string? DatosNuevos { get; set; }

    public string UsuarioBd { get; set; } = null!;

    public string? UsuarioApp { get; set; }

    public long Txid { get; set; }
}
