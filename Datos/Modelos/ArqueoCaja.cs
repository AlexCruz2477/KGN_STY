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

    [Column("total_ventas")]
    [Precision(12, 2)]
    public decimal TotalVentas { get; set; }

    [Column("total_egresos")]
    [Precision(12, 2)]
    public decimal TotalEgresos { get; set; }

    [Column("saldo_esperado")]
    [Precision(12, 2)]
    public decimal SaldoEsperado { get; set; }

    [Column("saldo_contado")]
    [Precision(12, 2)]
    public decimal SaldoContado { get; set; }

    [Column("diferencia")]
    [Precision(12, 2)]
    public decimal Diferencia { get; set; }

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
