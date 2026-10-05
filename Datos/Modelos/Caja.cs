using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("caja")]
[Index(nameof(IdUsuario), Name = "caja_id_usuario_key", IsUnique = true)]
[Index(nameof(NumeroCaja), Name = "ux_caja_numero_caja_ci", IsUnique = true)]
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

    [InverseProperty(nameof(AperturaCaja.IdCajaNavigation))]
    public virtual ICollection<AperturaCaja> AperturaCajas { get; set; } = new List<AperturaCaja>();

    [ForeignKey(nameof(IdUsuario))]
    [InverseProperty(nameof(Usuario.Caja))]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
