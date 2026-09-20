using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("caja")]
[Index("IdUsuario", Name = "caja_id_usuario_key", IsUnique = true)]
public partial class Caja
{
    [Key]
    [Column("id_caja")]
    public int IdCaja { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("numero_caja")]
    [StringLength(50)]
    public string NumeroCaja { get; set; } = null!;

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdCajaNavigation")]
    public virtual ICollection<AperturaCaja> AperturaCajas { get; set; } = new List<AperturaCaja>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Caja")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
