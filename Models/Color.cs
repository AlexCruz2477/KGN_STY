using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Color
{
    public int IdColor { get; set; }

    public string NombreColor { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
