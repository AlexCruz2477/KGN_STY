using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("apertura_caja")]
[Index(nameof(FechaApertura), Name = "idx_apertura_caja_activa_fecha", AllDescending = true)]
public partial class AperturaCaja
{
    [Key]
    [Column("id_apertura_caja")]
    public int IdAperturaCaja { get; set; }

    [Column("id_caja")]
    public int IdCaja { get; set; }

    [Column("fecha_apertura", TypeName = "timestamp without time zone")]
    public DateTime? FechaApertura { get; set; }

    [Column("fecha_cierre", TypeName = "timestamp without time zone")]
    public DateTime? FechaCierre { get; set; }

    [Column("monto_apertura")]
    [Precision(12, 2)]
    public decimal? MontoApertura { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty(nameof(ArqueoCaja.IdAperturaCajaNavigation))]
    public virtual ICollection<ArqueoCaja> ArqueoCajas { get; set; } = new List<ArqueoCaja>();

    [InverseProperty(nameof(Egreso.IdAperturaCajaNavigation))]
    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    [ForeignKey(nameof(IdCaja))]
    [InverseProperty(nameof(Caja.AperturaCajas))]
    public virtual Caja IdCajaNavigation { get; set; } = null!;

    [InverseProperty(nameof(Ventum.IdAperturaCajaNavigation))]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
