using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdAperturaCaja { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? NumeroComprobante { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Descuento { get; set; }

    public decimal TotalVenta { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
