using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [Required(ErrorMessage = "Calle es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre debe tener como máximo 50 caracteres.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Solo se aceptan letras")]
        public string? Calle { get; set; }
        [StringLength(10, ErrorMessage = "El telefono debe tener como máximo 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public string? NumeroInterior { get; set; }
        [StringLength(10, ErrorMessage = "El telefono debe tener como máximo 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se aceptan numeros")]
        public string? NumeroExterior { get; set; }

        public ML.Colonia Colonia { get; set; }
        public ML.Usuario? Usuario { get; set; }

    }
}
