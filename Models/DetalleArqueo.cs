using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class DetalleArqueo
{
    public int IdDetalleArqueo { get; set; }

    public int IdArqueo { get; set; }

    public decimal Denominacion { get; set; }

    public int Cantidad { get; set; }

    public decimal Subtotal { get; set; }

    public string Moneda { get; set; } = null!;

    public decimal TasaCambio { get; set; }

    public virtual ArqueoCaja IdArqueoNavigation { get; set; } = null!;
}
