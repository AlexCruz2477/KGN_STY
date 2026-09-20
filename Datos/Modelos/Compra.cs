using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("compra")]
public partial class Compra
{
    [Key]
    [Column("id_compra")]
    public int IdCompra { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("numero_factura")]
    [StringLength(50)]
    public string? NumeroFactura { get; set; }

    [Column("fecha_compra", TypeName = "timestamp without time zone")]
    public DateTime? FechaCompra { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal Subtotal { get; set; }

    [Column("impuesto")]
    [Precision(12, 2)]
    public decimal Impuesto { get; set; }

    [Column("total")]
    [Precision(12, 2)]
    public decimal Total { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdCompraNavigation")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [ForeignKey("IdProveedor")]
    [InverseProperty("Compras")]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Compras")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
