using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("log_sistema")]
[Index("Fecha", Name = "idx_log_sistema_fecha", AllDescending = true)]
[Index("Modulo", "Fecha", Name = "idx_log_sistema_modulo_fecha", IsDescending = new[] { false, true })]
[Index("TablaAfectada", "IdRegistro", Name = "idx_log_sistema_tabla_registro")]
public partial class LogSistema
{
    [Key]
    [Column("id_log")]
    public long IdLog { get; set; }

    [Column("fecha", TypeName = "timestamp without time zone")]
    public DateTime Fecha { get; set; }

    [Column("modulo")]
    [StringLength(30)]
    public string Modulo { get; set; } = null!;

    [Column("accion")]
    [StringLength(20)]
    public string Accion { get; set; } = null!;

    [Column("tabla_afectada")]
    [StringLength(80)]
    public string TablaAfectada { get; set; } = null!;

    [Column("id_registro")]
    public string? IdRegistro { get; set; }

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("datos_anteriores", TypeName = "jsonb")]
    public string? DatosAnteriores { get; set; }

    [Column("datos_nuevos", TypeName = "jsonb")]
    public string? DatosNuevos { get; set; }

    [Column("usuario_bd")]
    public string UsuarioBd { get; set; } = null!;

    [Column("usuario_app")]
    public string? UsuarioApp { get; set; }

    [Column("txid")]
    public long Txid { get; set; }
}
