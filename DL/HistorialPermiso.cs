using System;
using System.Collections.Generic;

namespace DL;

public partial class HistorialPermiso
{
    public int IdHistorialPermiso { get; set; }

    public int? IdPermiso { get; set; }

    public DateOnly FechaRevision { get; set; }

    public int IdStatusPermiso { get; set; }

    public string? Observaciones { get; set; }

    public int? AprovoRechazo { get; set; }

    public virtual Empleado? AprovoRechazoNavigation { get; set; }

    public virtual Permiso? IdPermisoNavigation { get; set; }

    public virtual StatusPermiso IdStatusPermisoNavigation { get; set; } = null!;
}
