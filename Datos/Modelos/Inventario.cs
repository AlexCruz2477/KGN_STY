using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("inventario")]
public partial class Inventario
{
    [Key]
    [Column("id_inventario")]
    public int IdInventario { get; set; }

    [Column("id_producto")]
    public int? IdProducto { get; set; }

    [Column("categoria")]
    [StringLength(100)]
    public string? Categoria { get; set; }

    [Column("stock_actual")]
    [Precision(10, 2)]
    public decimal? StockActual { get; set; }

    [Column("stock_minimo")]
    [Precision(10, 2)]
    public decimal? StockMinimo { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("Inventarios")]
    public virtual Producto? IdProductoNavigation { get; set; }
}
