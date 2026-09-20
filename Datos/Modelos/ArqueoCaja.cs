using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("arqueo_caja")]
public partial class ArqueoCaja
{
    [Key]
    [Column("id_arqueo")]
    public int IdArqueo { get; set; }

    [Column("id_apertura_caja")]
    public int IdAperturaCaja { get; set; }

    [Column("fecha_arqueo", TypeName = "timestamp without time zone")]
    public DateTime? FechaArqueo { get; set; }

    [Column("total_ventas_cordoba")]
    [Precision(12, 2)]
    public decimal TotalVentasCordoba { get; set; }

    [Column("total_ventas_dolar")]
    [Precision(12, 2)]
    public decimal TotalVentasDolar { get; set; }

    [Column("total_egresos_cordoba")]
    [Precision(12, 2)]
    public decimal TotalEgresosCordoba { get; set; }

    [Column("total_egresos_dolar")]
    [Precision(12, 2)]
    public decimal TotalEgresosDolar { get; set; }

    [Column("saldo_esperado_cordoba")]
    [Precision(12, 2)]
    public decimal SaldoEsperadoCordoba { get; set; }

    [Column("saldo_esperado_dolar")]
    [Precision(12, 2)]
    public decimal SaldoEsperadoDolar { get; set; }

    [Column("saldo_contado_cordoba")]
    [Precision(12, 2)]
    public decimal SaldoContadoCordoba { get; set; }

    [Column("saldo_contado_dolar")]
    [Precision(12, 2)]
    public decimal SaldoContadoDolar { get; set; }

    [Column("diferencia_cordoba")]
    [Precision(12, 2)]
    public decimal DiferenciaCordoba { get; set; }

    [Column("diferencia_dolar")]
    [Precision(12, 2)]
    public decimal DiferenciaDolar { get; set; }

    [Column("observacion")]
    [StringLength(250)]
    public string? Observacion { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdArqueoNavigation")]
    public virtual ICollection<DetalleArqueo> DetalleArqueos { get; set; } = new List<DetalleArqueo>();

    [ForeignKey("IdAperturaCaja")]
    [InverseProperty("ArqueoCajas")]
    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;
}
