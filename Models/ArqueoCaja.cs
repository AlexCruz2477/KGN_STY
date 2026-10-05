using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class ArqueoCaja
{
    public int IdArqueo { get; set; }

    public int IdAperturaCaja { get; set; }

    public DateTime? FechaArqueo { get; set; }

    public decimal TotalVentas { get; set; }

    public decimal TotalEgresos { get; set; }

    public decimal SaldoEsperado { get; set; }

    public decimal SaldoContado { get; set; }

    public decimal Diferencia { get; set; }

    public string? Observacion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleArqueo> DetalleArqueos { get; set; } = new List<DetalleArqueo>();

    public virtual AperturaCaja IdAperturaCajaNavigation { get; set; } = null!;
}
