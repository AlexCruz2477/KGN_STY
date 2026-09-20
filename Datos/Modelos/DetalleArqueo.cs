using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("detalle_arqueo")]
public partial class DetalleArqueo
{
    [Key]
    [Column("id_detalle_arqueo")]
    public int IdDetalleArqueo { get; set; }

    [Column("id_arqueo")]
    public int IdArqueo { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("denominacion")]
    [Precision(12, 2)]
    public decimal Denominacion { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("subtotal")]
    [Precision(12, 2)]
    public decimal Subtotal { get; set; }

    [ForeignKey("IdArqueo")]
    [InverseProperty("DetalleArqueos")]
    public virtual ArqueoCaja IdArqueoNavigation { get; set; } = null!;

    [ForeignKey("IdMoneda")]
    [InverseProperty("DetalleArqueos")]
    public virtual Monedum IdMonedaNavigation { get; set; } = null!;
}
