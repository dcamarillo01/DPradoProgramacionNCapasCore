using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {

        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? RFC { get; set; }
        public string? NSS { get; set; }
        public string? CURP { get; set; }
        public string? FechaIngreso { get; set; }
        public ML.Departamento? Departamento { get; set; }
        public int? SalarioBase { get; set; }
        public int? NoFaltas { get; set; }

    }
}
