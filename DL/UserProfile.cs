using System;
using System.Collections.Generic;

namespace DL;

public partial class UserProfile
{
    public int IdUserProfile { get; set; }

    public int? IdEmpleado { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? Status { get; set; }

    public int? IdRol { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
