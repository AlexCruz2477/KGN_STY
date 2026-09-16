using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("devolucion_venta")]
public partial class DevolucionVentum
{
    [Key]
    [Column("id_devolucion_venta")]
    public int IdDevolucionVenta { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("id_venta")]
    public int? IdVenta { get; set; }

    [Column("tipo_devolucion")]
    [StringLength(100)]
    public string? TipoDevolucion { get; set; }

    [Column("motivo")]
    [StringLength(100)]
    public string? Motivo { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdDevolucionVentaNavigation")]
    public virtual ICollection<DetalleDevolucionVentum> DetalleDevolucionVenta { get; set; } = new List<DetalleDevolucionVentum>();

    [ForeignKey("IdCliente")]
    [InverseProperty("DevolucionVenta")]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("DevolucionVenta")]
    public virtual Ventum? IdVentaNavigation { get; set; }
}
