using System;
using System.Collections.Generic;

namespace DL;

public partial class VwUserProfile
{
    public int IdUserProfile { get; set; }

    public int? IdEmpleado { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? Status { get; set; }

    public int? IdRol { get; set; }

    public string? RolType { get; set; }
}
