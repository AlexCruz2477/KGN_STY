using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("categoria")]
public partial class Categorium
{
    [Key]
    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("nombre_categoria")]
    [StringLength(100)]
    public string? NombreCategoria { get; set; }

    [Column("decripcion")]
    [StringLength(100)]
    public string? Decripcion { get; set; }

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<TipoProducto> TipoProductos { get; set; } = new List<TipoProducto>();
}
