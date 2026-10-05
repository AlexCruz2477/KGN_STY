using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Egreso
{
    public int IdEgreso { get; set; }

    public int IdAperturaCaja { get; set; }

    public int IdTipoEgreso { get; set; }

    public decimal Monto { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaEgreso { get; set; }

    public bool? Estado { get; set; }

    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;

    public virtual TipoEgreso IdTipoEgresoNavigation { get; set; } = null!;
}
