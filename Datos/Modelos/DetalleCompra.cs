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
    public int IdCompra { get; set; }

    [Column("id_variante")]
    public int IdVariante { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(12, 2)]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal? Subtotal { get; set; }

    [ForeignKey("IdCompra")]
    [InverseProperty("DetalleCompras")]
    public virtual Compra IdCompraNavigation { get; set; } = null!;

    [ForeignKey("IdVariante")]
    [InverseProperty("DetalleCompras")]
    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
