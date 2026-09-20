using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("producto_variante")]
[Index("Codigo", Name = "producto_variante_codigo_key", IsUnique = true)]
public partial class ProductoVariante
{
    [Key]
    [Column("id_variante")]
    public int IdVariante { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_talla")]
    public int? IdTalla { get; set; }

    [Column("id_color")]
    public int? IdColor { get; set; }

    [Column("codigo")]
    [StringLength(50)]
    public string Codigo { get; set; } = null!;

    [Column("stock_actual")]
    public int StockActual { get; set; }

    [Column("stock_minimo")]
    public int StockMinimo { get; set; }

    [Column("precio_compra")]
    [Precision(12, 2)]
    public decimal PrecioCompra { get; set; }

    [Column("precio_venta")]
    [Precision(12, 2)]
    public decimal PrecioVenta { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdVarianteNavigation")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [InverseProperty("IdVarianteNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [ForeignKey("IdColor")]
    [InverseProperty("ProductoVariantes")]
    public virtual Color? IdColorNavigation { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("ProductoVariantes")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdTalla")]
    [InverseProperty("ProductoVariantes")]
    public virtual Talla? IdTallaNavigation { get; set; }
}
