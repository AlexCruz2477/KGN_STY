using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class PagoVentum
{
    public int IdPagoVenta { get; set; }

    public int IdVenta { get; set; }

    public int IdMetodoPago { get; set; }

    public decimal Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
