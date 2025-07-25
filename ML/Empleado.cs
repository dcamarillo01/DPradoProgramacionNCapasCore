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
        [RegularExpression(@"[A-z]{4}[0-9]{6}[A-z0-9]{3}", ErrorMessage = "RFC Invalido")]
        public string? RFC { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public string? NSS { get; set; }
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "CURP es invalido")]
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
