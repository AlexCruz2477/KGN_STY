using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Caja
{
    public int IdCaja { get; set; }

    public int IdUsuario { get; set; }

    public string NumeroCaja { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual AperturaCaja? AperturaCaja { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
