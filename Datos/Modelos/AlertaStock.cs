using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("alerta_stock")]
public partial class AlertaStock
{
    [Key]
    [Column("id_alerta")]
    public long IdAlerta { get; set; }

    [Column("id_variante")]
    public int IdVariante { get; set; }

    [Column("stock_actual")]
    public int StockActual { get; set; }

    [Column("stock_minimo")]
    public int StockMinimo { get; set; }

    [Column("fecha_alerta", TypeName = "timestamp without time zone")]
    public DateTime FechaAlerta { get; set; }

    [Column("atendida")]
    public bool Atendida { get; set; }

    [Column("fecha_atendida", TypeName = "timestamp without time zone")]
    public DateTime? FechaAtendida { get; set; }

    [ForeignKey("IdVariante")]
    [InverseProperty("AlertaStocks")]
    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
