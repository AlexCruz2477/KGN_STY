using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("venta")]
public partial class Ventum
{
    [Key]
    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_apertura_caja")]
    public int IdAperturaCaja { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("fecha_venta", TypeName = "timestamp without time zone")]
    public DateTime? FechaVenta { get; set; }

    [Column("numero_comprobante")]
    [StringLength(50)]
    public string? NumeroComprobante { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal Subtotal { get; set; }

    [Column("iva")]
    [Precision(12, 2)]
    public decimal Iva { get; set; }

    [Column("descuento")]
    [Precision(12, 2)]
    public decimal Descuento { get; set; }

    [Column("total_venta")]
    [Precision(12, 2)]
    public decimal TotalVenta { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [ForeignKey("IdAperturaCaja")]
    [InverseProperty("Venta")]
    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("Venta")]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
