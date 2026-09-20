using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("talla")]
public partial class Talla
{
    [Key]
    [Column("id_talla")]
    public int IdTalla { get; set; }

    [Column("nombre_talla")]
    [StringLength(50)]
    public string NombreTalla { get; set; } = null!;

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdTallaNavigation")]
    public virtual ICollection<ProductoVariante> ProductoVariantes { get; set; } = new List<ProductoVariante>();
}
