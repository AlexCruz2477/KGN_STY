using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdMarca { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
