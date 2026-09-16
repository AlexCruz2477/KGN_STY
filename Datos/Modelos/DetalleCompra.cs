using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_compra")]
public partial class DetalleCompra
{
    [Key]
    [Column("id_detalle_compra")]
    public int IdDetalleCompra { get; set; }

    [Column("id_compra")]
    public int? IdCompra { get; set; }

    [Column("id_producto")]
    public int? IdProducto { get; set; }

    [Column("cantidad")]
    [Precision(10, 2)]
    public decimal Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(12, 2)]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal Subtotal { get; set; }

    [ForeignKey("IdCompra")]
    [InverseProperty("DetalleCompras")]
    public virtual Compra? IdCompraNavigation { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleCompras")]
    public virtual Producto? IdProductoNavigation { get; set; }
}
