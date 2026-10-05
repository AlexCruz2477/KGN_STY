using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int IdProveedor { get; set; }

    public int IdUsuario { get; set; }

    public string? NumeroFactura { get; set; }

    public DateTime? FechaCompra { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Impuesto { get; set; }

    public decimal Total { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
