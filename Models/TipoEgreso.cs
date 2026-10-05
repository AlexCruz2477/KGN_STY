using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class TipoEgreso
{
    public int IdTipoEgreso { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();
}
