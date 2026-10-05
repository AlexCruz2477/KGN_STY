using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class AlertaStock
{
    public long IdAlerta { get; set; }

    public int IdVariante { get; set; }

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public DateTime FechaAlerta { get; set; }

    public bool Atendida { get; set; }

    public DateTime? FechaAtendida { get; set; }

    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
