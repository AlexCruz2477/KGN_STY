using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_devolucion_venta")]
public partial class DetalleDevolucionVentum
{
    [Key]
    [Column("id_detalle_dev_venta")]
    public int IdDetalleDevVenta { get; set; }

    [Column("id_devolucion_venta")]
    public int? IdDevolucionVenta { get; set; }

    [Column("id_producto")]
    public int? IdProducto { get; set; }

    [Column("cantidad_vendida")]
    [Precision(10, 2)]
    public decimal CantidadVendida { get; set; }

    [Column("cantidad_devolver")]
    [Precision(10, 2)]
    public decimal CantidadDevolver { get; set; }

    [Column("precio_unitario")]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [ForeignKey("IdDevolucionVenta")]
    [InverseProperty("DetalleDevolucionVenta")]
    public virtual DevolucionVentum? IdDevolucionVentaNavigation { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleDevolucionVenta")]
    public virtual Producto? IdProductoNavigation { get; set; }
}
