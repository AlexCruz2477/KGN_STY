using System;
using System.Collections.Generic;

namespace Nk_Colletion_New.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Correo { get; set; }

    public bool Estado { get; set; }

    public string? TokenRecuperacion { get; set; }

    public DateTime? FechaHoraRecuperacion { get; set; }

    public virtual Caja? Caja { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
