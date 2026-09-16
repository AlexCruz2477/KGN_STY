using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("credito")]
public partial class Credito
{
    [Key]
    [Column("id_credito")]
    public int IdCredito { get; set; }

    [Column("id_venta")]
    public int? IdVenta { get; set; }

    [Column("fecha_solicitud", TypeName = "timestamp without time zone")]
    public DateTime? FechaSolicitud { get; set; }

    [Column("monto_credito")]
    [Precision(10, 2)]
    public decimal? MontoCredito { get; set; }

    [Column("plazo")]
    public DateOnly? Plazo { get; set; }

    [Column("tasa_interes")]
    [Precision(5, 2)]
    public decimal? TasaInteres { get; set; }

    [Column("fecha_vencimiento", TypeName = "timestamp without time zone")]
    public DateTime? FechaVencimiento { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdCreditoNavigation")]
    public virtual ICollection<DetallePagoCredito> DetallePagoCreditos { get; set; } = new List<DetallePagoCredito>();

    [ForeignKey("IdVenta")]
    [InverseProperty("Creditos")]
    public virtual Ventum? IdVentaNavigation { get; set; }
}
