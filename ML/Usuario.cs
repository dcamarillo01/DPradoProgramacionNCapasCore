using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(30, ErrorMessage = "El apellido debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? ApellidoPaterno { get; set; }
        [StringLength(30, ErrorMessage = "El apellido debe tener como máximo 30 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "El Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo invalido.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "UserName es obligatorio.")]
        [StringLength(30, ErrorMessage = "UserName debe tener máximo 30 caracteres.")]
        [RegularExpression(@"^[A-Za-z0-9!@@#%$^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$", ErrorMessage = "No se permiten espacios")]
        //@"^[A-Za-z0-9!@@#%$^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$"
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password es obligatorio.")]
        [StringLength(16, ErrorMessage = "Password debe tener máximo 30 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@@#$%^&*])[a-zA-Z0-9!@@#$%^&*]{6,16}$", ErrorMessage = "Solo se aceptan letras")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Sexo es obligatorio.")]
        public string? Sexo { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [StringLength(10, ErrorMessage = "El telefono debe tener como máximo 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public string? Telefono { get; set; }
        [StringLength(10, ErrorMessage = "El celular debe tener como máximo 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public string? Celular { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public string? FechaNacimiento { get; set; }
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "CURP es invalido")]
        public string? Curp { get; set; }
        public ML.Rol? Rol { get; set; }

        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(AllowEmptyStrings = true)]

        public byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public List<object>? Usuarios { get; set; }
        public ML.Direccion? Direccion { get; set; }

        public bool Status { get; set; }

        


    }
}
