using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class TipoCambioBcn
{
    public DateOnly Fecha { get; set; }

    public decimal TasaNioPorUsd { get; set; }

    public string Fuente { get; set; } = null!;

    public string? Referencia { get; set; }

    public bool Oficial { get; set; }

    public DateTime FechaRegistro { get; set; }
}
