using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("proveedor")]
[Index("Ruc", Name = "proveedor_ruc_key", IsUnique = true)]
public partial class Proveedor
{
    [Key]
    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [Column("direccion")]
    [StringLength(200)]
    public string? Direccion { get; set; }

    [Column("ruc")]
    [StringLength(50)]
    public string? Ruc { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [Column("fecha_registro", TypeName = "timestamp without time zone")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
