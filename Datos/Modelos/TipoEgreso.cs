using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("tipo_egreso")]
public partial class TipoEgreso
{
    [Key]
    [Column("id_tipo_egreso")]
    public int IdTipoEgreso { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(150)]
    public string? Descripcion { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdTipoEgresoNavigation")]
    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();
}
