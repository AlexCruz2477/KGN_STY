using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("movimiento_inventario")]
[Index("ReferenciaTabla", "ReferenciaId", Name = "idx_movimiento_inventario_referencia")]
[Index("IdVariante", "FechaMovimiento", Name = "idx_movimiento_inventario_variante_fecha", IsDescending = new[] { false, true })]
public partial class MovimientoInventario
{
    [Key]
    [Column("id_movimiento")]
    public long IdMovimiento { get; set; }

    [Column("id_variante")]
    public int IdVariante { get; set; }

    [Column("tipo_movimiento")]
    [StringLength(50)]
    public string TipoMovimiento { get; set; } = null!;

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("stock_anterior")]
    public int StockAnterior { get; set; }

    [Column("stock_nuevo")]
    public int StockNuevo { get; set; }

    [Column("referencia_tabla")]
    [StringLength(50)]
    public string? ReferenciaTabla { get; set; }

    [Column("referencia_id")]
    public int? ReferenciaId { get; set; }

    [Column("id_detalle")]
    public int? IdDetalle { get; set; }

    [Column("fecha_movimiento", TypeName = "timestamp without time zone")]
    public DateTime FechaMovimiento { get; set; }

    [Column("usuario_bd")]
    public string UsuarioBd { get; set; } = null!;

    [Column("txid")]
    public long Txid { get; set; }

    [ForeignKey("IdVariante")]
    [InverseProperty("MovimientoInventarios")]
    public virtual ProductoVariante IdVarianteNavigation { get; set; } = null!;
}
