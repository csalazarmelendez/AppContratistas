using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class ValidarDatosTrabajador
    {

        [Required(ErrorMessage = "Cédula es obligatorio")]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Email es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Teléfono es obligatorio")]
        [StringLength(25, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Telefono { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string PersonaContacto { get; set; }

        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string TelefonoContacto { get; set; }

        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Ciudad { get; set; }

        public string EstadoIngreso { get; set; }
    
        [Required(ErrorMessage = "Archivo cédula es obligatorio")]
        public IFormFile CedulaImg { get; set; }

        [Required(ErrorMessage = "Archivo EPS es obligatorio")]
        public IFormFile EPS { get; set; }

        [Required(ErrorMessage = "Archivo ARL es obligatorio")]
        public IFormFile ARL { get; set; }

        [Required(ErrorMessage = "Archivo pensión es obligatorio")]
        public IFormFile Pension { get; set; }
    }
}

