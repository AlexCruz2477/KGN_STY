using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("pago_venta")]
public partial class PagoVentum
{
    [Key]
    [Column("id_pago_venta")]
    public int IdPagoVenta { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_metodo_pago")]
    public int IdMetodoPago { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("id_tipo_cambio")]
    public int IdTipoCambio { get; set; }

    [Column("monto")]
    [Precision(12, 2)]
    public decimal Monto { get; set; }

    [Column("fecha_pago", TypeName = "timestamp without time zone")]
    public DateTime? FechaPago { get; set; }

    [ForeignKey("IdMetodoPago")]
    [InverseProperty("PagoVenta")]
    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    [ForeignKey("IdMoneda")]
    [InverseProperty("PagoVenta")]
    public virtual Monedum IdMonedaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoCambio")]
    [InverseProperty("PagoVenta")]
    public virtual TipoCambio IdTipoCambioNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("PagoVenta")]
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
