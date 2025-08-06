using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Permiso
    {

        public int IdPermiso { get; set; }

        public ML.Empleado? Empleado { get; set; }

        public string? FechaSolicitud { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin{ get; set; }
        public TimeOnly? HoraInicio { get; set; }
        public TimeOnly? HoraFin { get; set; }
        public string? Motivo { get; set; }

        public ML.StatusPermiso? StatusPermiso { get; set; }
        public ML.Empleado? EmpleadoAutorizador { get; set; }


    }
}
