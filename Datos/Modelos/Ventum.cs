using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("venta")]
public partial class Ventum
{
    [Key]
    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("fecha_venta")]
    public DateOnly? FechaVenta { get; set; }

    [Column("numero_comprobante")]
    [Precision(12, 2)]
    public decimal? NumeroComprobante { get; set; }

    [Column("iva")]
    [Precision(10, 2)]
    public decimal? Iva { get; set; }

    [Column("total_venta")]
    [Precision(12, 2)]
    public decimal? TotalVenta { get; set; }

    [Column("metodo_pago")]
    [StringLength(76)]
    public string? MetodoPago { get; set; }

    [Column("descuento")]
    [Precision(12, 2)]
    public decimal? Descuento { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Credito> Creditos { get; set; } = new List<Credito>();

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DevolucionVentum> DevolucionVenta { get; set; } = new List<DevolucionVentum>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Venta")]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Venta")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
