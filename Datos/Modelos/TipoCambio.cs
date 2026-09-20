using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("tipo_cambio")]
public partial class TipoCambio
{
    [Key]
    [Column("id_tipo_cambio")]
    public int IdTipoCambio { get; set; }

    [Column("valor_dolar")]
    [Precision(12, 4)]
    public decimal ValorDolar { get; set; }

    [Column("fecha_registro", TypeName = "timestamp without time zone")]
    public DateTime? FechaRegistro { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdTipoCambioNavigation")]
    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    [InverseProperty("IdTipoCambioNavigation")]
    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
