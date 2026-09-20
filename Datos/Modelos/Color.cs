using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("color")]
public partial class Color
{
    [Key]
    [Column("id_color")]
    public int IdColor { get; set; }

    [Column("nombre_color")]
    [StringLength(50)]
    public string NombreColor { get; set; } = null!;

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdColorNavigation")]
    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
