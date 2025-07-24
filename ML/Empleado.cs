using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {

        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? ApellidoPaterno { get; set; }
        [StringLength(30, ErrorMessage = "El nombre debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? ApellidoMaterno { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? RFC { get; set; }
        public string? NSS { get; set; }
        public string? CURP { get; set; }
        public string? FechaIngreso { get; set; }
        public ML.Departamento? Departamento { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public int? SalarioBase { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public int? NoFaltas { get; set; }
        public List<object>? Empleados { get; set; }
    }
}
