using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_pago_credito")]
public partial class DetallePagoCredito
{
    [Key]
    [Column("id_detalle_pago_credito")]
    public int IdDetallePagoCredito { get; set; }

    [Column("id_credito")]
    public int? IdCredito { get; set; }

    [Column("fecha_pago", TypeName = "timestamp without time zone")]
    public DateTime? FechaPago { get; set; }

    [Column("monto_pago")]
    [Precision(10, 2)]
    public decimal MontoPago { get; set; }

    [Column("saldo_restante")]
    [Precision(10, 2)]
    public decimal SaldoRestante { get; set; }

    [InverseProperty("IdDetallePagoCreditoNavigation")]
    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();

    [ForeignKey("IdCredito")]
    [InverseProperty("DetallePagoCreditos")]
    public virtual Credito? IdCreditoNavigation { get; set; }
}
