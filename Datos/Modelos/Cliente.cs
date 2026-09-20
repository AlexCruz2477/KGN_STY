using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("cliente")]
[Index("Cedula", Name = "cliente_cedula_key", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("cedula")]
    [StringLength(20)]
    public string? Cedula { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [Column("direccion")]
    [StringLength(200)]
    public string? Direccion { get; set; }

    [Column("fecha_registro", TypeName = "timestamp without time zone")]
    public DateTime? FechaRegistro { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
