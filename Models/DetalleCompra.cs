using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int IdCompra { get; set; }

    public int IdVariante { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
