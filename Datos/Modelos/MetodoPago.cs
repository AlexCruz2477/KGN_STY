using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("metodo_pago")]
public partial class MetodoPago
{
    [Key]
    [Column("id_metodo_pago")]
    public int IdMetodoPago { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdMetodoPagoNavigation")]
    public virtual ICollection<PagoVentum> PagoVenta { get; set; } = new List<PagoVentum>();
}
