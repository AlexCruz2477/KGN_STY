using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("usuario")]
[Index("Cedula", Name = "usuario_cedula_key", IsUnique = true)]
[Index("Correo", Name = "usuario_correo_key", IsUnique = true)]
[Index("Usuario1", Name = "usuario_usuario_key", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("cedula")]
    [StringLength(20)]
    public string Cedula { get; set; } = null!;

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("usuario")]
    [StringLength(50)]
    public string Usuario1 { get; set; } = null!;

    [Column("contrasena")]
    [StringLength(255)]
    public string Contrasena { get; set; } = null!;

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("token_recuperacion")]
    [StringLength(255)]
    public string? TokenRecuperacion { get; set; }

    [Column("fecha_hora_recuperacion", TypeName = "timestamp without time zone")]
    public DateTime? FechaHoraRecuperacion { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual Caja? Caja { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Rol IdRolNavigation { get; set; } = null!;
}
