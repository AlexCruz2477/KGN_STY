using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Talla
{
    public int IdTalla { get; set; }

    public string NombreTalla { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
