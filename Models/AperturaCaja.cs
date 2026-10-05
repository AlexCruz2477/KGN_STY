using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class AperturaCaja
{
    public int IdAperturaCaja { get; set; }

    public int IdCaja { get; set; }

    public DateTime? FechaApertura { get; set; }

    public DateTime? FechaCierre { get; set; }

    public decimal? MontoApertura { get; set; }

    public bool? Estado { get; set; }

    public virtual ArqueoCaja? ArqueoCaja { get; set; }

    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    public virtual Caja IdCajaNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
