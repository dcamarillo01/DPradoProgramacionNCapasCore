using System;
using System.Collections.Generic;

namespace DL;

public partial class VwEmpleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string? FechaNacimiento { get; set; }

    public string Rfc { get; set; } = null!;

    public string Nss { get; set; } = null!;

    public string? Curp { get; set; }

    public string? FechaIngreso { get; set; }

    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public decimal? SalarioBase { get; set; }

    public int? NoFaltas { get; set; }
}
