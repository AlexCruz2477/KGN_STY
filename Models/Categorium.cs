using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
