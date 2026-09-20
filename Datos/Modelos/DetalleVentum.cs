using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_venta")]
public partial class DetalleVentum
{
    [Key]
    [Column("id_detalle_venta")]
    public int IdDetalleVenta { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

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

    [ForeignKey("IdVariante")]
    [InverseProperty("DetalleVenta")]
    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("DetalleVenta")]
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
