using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
