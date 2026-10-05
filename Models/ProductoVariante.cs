using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class ProductoVariante
{
    public int IdVariante { get; set; }

    public int IdProducto { get; set; }

    public int? IdTalla { get; set; }

    public int? IdColor { get; set; }

    public string Codigo { get; set; } = null!;

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public decimal PrecioCompra { get; set; }

    public decimal PrecioVenta { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<AlertaStock> AlertaStocks { get; set; } = new List<AlertaStock>();

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Color? IdColorNavigation { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Talla? IdTallaNavigation { get; set; }

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();
}
