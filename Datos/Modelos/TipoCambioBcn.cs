using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("tipo_cambio_bcn")]
public partial class TipoCambioBcn
{
    [Key]
    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("tasa_nio_por_usd")]
    [Precision(12, 4)]
    public decimal TasaNioPorUsd { get; set; }

    [Column("fuente")]
    [StringLength(120)]
    public string Fuente { get; set; } = null!;

    [Column("referencia")]
    public string? Referencia { get; set; }

    [Column("oficial")]
    public bool Oficial { get; set; }

    [Column("fecha_registro", TypeName = "timestamp without time zone")]
    public DateTime FechaRegistro { get; set; }
}
