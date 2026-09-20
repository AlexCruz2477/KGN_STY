using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("producto")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_categoria")]
    public int? IdCategoria { get; set; }

    [Column("id_marca")]
    public int? IdMarca { get; set; }

    [Column("nombre_producto")]
    [StringLength(150)]
    public string NombreProducto { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium? IdCategoriaNavigation { get; set; }

    [ForeignKey("IdMarca")]
    [InverseProperty("Productos")]
    public virtual Marca? IdMarcaNavigation { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
