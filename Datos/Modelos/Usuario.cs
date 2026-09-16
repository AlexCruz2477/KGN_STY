using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Datos.Modelos;

[Table("usuario")]
[Index("Correo", Name = "usuario_correo_key", IsUnique = true)]
[Index("Usuario1", Name = "usuario_usuario_key", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int? IdRol { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("usuario")]
    [StringLength(50)]
    public string? Usuario1 { get; set; }

    [Column("contrasena")]
    [StringLength(50)]
    public string Contrasena { get; set; } = null!;

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Rol? IdRolNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
