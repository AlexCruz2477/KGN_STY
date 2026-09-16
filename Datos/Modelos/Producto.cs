using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("producto")]
[Index("Codigo", Name = "producto_codigo_key", IsUnique = true)]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_categoria")]
    public int? IdCategoria { get; set; }

    [Column("id_marca")]
    public int? IdMarca { get; set; }

    [Column("id_tipo_producto")]
    public int? IdTipoProducto { get; set; }

    [Column("talla")]
    [StringLength(20)]
    public string? Talla { get; set; }

    [Column("color")]
    [StringLength(50)]
    public string? Color { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("codigo")]
    [StringLength(50)]
    public string Codigo { get; set; } = null!;

    [Column("stock_minimo")]
    public int StockMinimo { get; set; }

    [Column("precio_venta")]
    [Precision(10, 2)]
    public decimal PrecioVenta { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleDevolucionVentum> DetalleDevolucionVenta { get; set; } = new List<DetalleDevolucionVentum>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium? IdCategoriaNavigation { get; set; }

    [ForeignKey("IdMarca")]
    [InverseProperty("Productos")]
    public virtual Marca? IdMarcaNavigation { get; set; }

    [ForeignKey("IdTipoProducto")]
    [InverseProperty("Productos")]
    public virtual TipoProducto? IdTipoProductoNavigation { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
