using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_arqueo")]
[Index("IdArqueo", Name = "idx_detalle_arqueo_id_arqueo")]
public partial class DetalleArqueo
{
    [Key]
    [Column("id_detalle_arqueo")]
    public int IdDetalleArqueo { get; set; }

    [Column("id_arqueo")]
    public int IdArqueo { get; set; }

    [Column("denominacion")]
    [Precision(12, 2)]
    public decimal Denominacion { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal Subtotal { get; set; }

    [Column("moneda")]
    [StringLength(3)]
    public string Moneda { get; set; } = null!;

    [Column("tasa_cambio")]
    [Precision(12, 4)]
    public decimal TasaCambio { get; set; }

    [ForeignKey("IdArqueo")]
    [InverseProperty("DetalleArqueos")]
    public virtual ArqueoCaja IdArqueoNavigation { get; set; } = null!;
}
