using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("marca")]
[Index("Nombre", Name = "marca_nombre_key", IsUnique = true)]
public partial class Marca
{
    [Key]
    [Column("id_marca")]
    public int IdMarca { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("estado")]
    public bool Estado { get; set; }

    [InverseProperty("IdMarcaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
