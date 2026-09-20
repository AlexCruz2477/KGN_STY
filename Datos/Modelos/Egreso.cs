using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("egreso")]
public partial class Egreso
{
    [Key]
    [Column("id_egreso")]
    public int IdEgreso { get; set; }

    [Column("id_apertura_caja")]
    public int IdAperturaCaja { get; set; }

    [Column("id_tipo_egreso")]
    public int IdTipoEgreso { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("id_tipo_cambio")]
    public int IdTipoCambio { get; set; }

    [Column("monto")]
    [Precision(12, 2)]
    public decimal Monto { get; set; }

    [Column("descripcion")]
    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Column("fecha_egreso", TypeName = "timestamp without time zone")]
    public DateTime? FechaEgreso { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [ForeignKey("IdAperturaCaja")]
    [InverseProperty("Egresos")]
    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;

    [ForeignKey("IdMoneda")]
    [InverseProperty("Egresos")]
    public virtual Monedum IdMonedaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoCambio")]
    [InverseProperty("Egresos")]
    public virtual TipoCambio IdTipoCambioNavigation { get; set; } = null!;

    [ForeignKey("IdTipoEgreso")]
    [InverseProperty("Egresos")]
    public virtual TipoEgreso IdTipoEgresoNavigation { get; set; } = null!;
}
