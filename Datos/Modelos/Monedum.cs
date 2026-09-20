using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("moneda")]
[Index("Codigo", Name = "moneda_codigo_key", IsUnique = true)]
public partial class Monedum
{
    [Key]
    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("codigo")]
    [StringLength(10)]
    public string Codigo { get; set; } = null!;

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("simbolo")]
    [StringLength(10)]
    public string? Simbolo { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<DetalleArqueo> DetalleArqueos { get; set; } = new List<DetalleArqueo>();

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
