using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("tipo_producto")]
public partial class TipoProducto
{
    [Key]
    [Column("id_tipo_producto")]
    public int IdTipoProducto { get; set; }

    [Column("nombre_tipo")]
    [StringLength(100)]
    public string NombreTipo { get; set; } = null!;

    [Column("id_categoria")]
    public int? IdCategoria { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("TipoProductos")]
    public virtual Categorium? IdCategoriaNavigation { get; set; }

    [InverseProperty("IdTipoProductoNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
