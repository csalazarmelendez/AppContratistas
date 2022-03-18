using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class SolicitudRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Razón social es obligatorio")]
        [StringLength(30, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string NIT { get; set; }

        [Required(ErrorMessage = "Razón social es obligatorio")]
        [StringLength(70, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Razón social")]
        public string Razon_social { get; set; }

        [Required(ErrorMessage = "Email es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Usuario es obligatorio")]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Constraseña")]
        public string Contrasena { get; set; }

        [StringLength(15, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 5)]
        [Display(Name = "Transacción")]
        public string Transaccion { get; set; }

        [Required(ErrorMessage = "Nombre de operador es obligatorio")]
        [Display(Name = "Operador")]
        public string Operador { get; set; }

        [Required(ErrorMessage = "Número de operador es obligatorio")]
        [Display(Name = "Número de operador")]
        public string NumeroOperador { get; set; }


    }
}
