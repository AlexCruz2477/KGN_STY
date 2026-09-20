using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("apertura_caja")]
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

    [Column("monto_apertura_cordoba")]
    [Precision(12, 2)]
    public decimal? MontoAperturaCordoba { get; set; }

    [Column("monto_apertura_dolar")]
    [Precision(12, 2)]
    public decimal? MontoAperturaDolar { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdAperturaCajaNavigation")]
    public virtual ICollection<ArqueoCaja> ArqueoCajas { get; set; } = new List<ArqueoCaja>();

    [InverseProperty("IdAperturaCajaNavigation")]
    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    [ForeignKey("IdCaja")]
    [InverseProperty("AperturaCajas")]
    public virtual Caja IdCajaNavigation { get; set; } = null!;

    [InverseProperty("IdAperturaCajaNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
