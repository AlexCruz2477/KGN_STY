using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("caja")]
public partial class Caja
{
    [Key]
    [Column("id_movimiento")]
    public int IdMovimiento { get; set; }

    [Column("id_venta")]
    public int? IdVenta { get; set; }

    [Column("id_compra")]
    public int? IdCompra { get; set; }

    [Column("id_detalle_pago_credito")]
    public int? IdDetallePagoCredito { get; set; }

    [Column("tipo_movimiento")]
    [StringLength(100)]
    public string? TipoMovimiento { get; set; }

    [Column("monto")]
    [Precision(10, 2)]
    public decimal? Monto { get; set; }

    [Column("ingresos")]
    [Precision(10, 2)]
    public decimal? Ingresos { get; set; }

    [Column("egresos")]
    [Precision(10, 2)]
    public decimal? Egresos { get; set; }

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("fecha", TypeName = "timestamp without time zone")]
    public DateTime? Fecha { get; set; }

    [ForeignKey("IdCompra")]
    [InverseProperty("Cajas")]
    public virtual Compra? IdCompraNavigation { get; set; }

    [ForeignKey("IdDetallePagoCredito")]
    [InverseProperty("Cajas")]
    public virtual DetallePagoCredito? IdDetallePagoCreditoNavigation { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("Cajas")]
    public virtual Ventum? IdVentaNavigation { get; set; }
}
