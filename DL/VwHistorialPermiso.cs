using System;
using System.Collections.Generic;

namespace DL;

public partial class VwHistorialPermiso
{
    public int IdHistorialPermiso { get; set; }

    public int? IdPermiso { get; set; }

    public string? FechaRevision { get; set; }

    public string? Descripcion { get; set; }

    public string? Observaciones { get; set; }

    public int IdEmpleado { get; set; }

    public string? Autorizo { get; set; }
}
