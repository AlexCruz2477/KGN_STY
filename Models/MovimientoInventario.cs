using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class MovimientoInventario
{
    public long IdMovimiento { get; set; }

    public int IdVariante { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public int StockAnterior { get; set; }

    public int StockNuevo { get; set; }

    public string? ReferenciaTabla { get; set; }

    public int? ReferenciaId { get; set; }

    public int? IdDetalle { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public string UsuarioBd { get; set; } = null!;

    public long Txid { get; set; }

    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
