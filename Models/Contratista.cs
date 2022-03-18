using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class Contratista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "NIT es obligatorio")]
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

        [Required(ErrorMessage = "Contrasena es obligatoria")]
        [StringLength(100, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Constraseña")]
        public string Contrasena { get; set; }

        [Required]
        [Display(Name = "Ingreso al portal")]
        public bool ingresoPortal { get; set; }

        public string Observacion { get; set; }

        [Required]
        public char ins_mod_elim { get; set; }

        [ForeignKey("Administradores")]
        public int Responsable { get; set; }

        public virtual Administrador Administradores { get; set; }

        [ForeignKey("Operadores")]
        public int IdOperador { get; set; }

        public virtual Operador Operadores { get; set; }

        [ForeignKey("SolicitudRegistros")]
        public int IdSolicitudRegistro { get; set; }

        public virtual SolicitudRegistro SolicitudRegistros { get; set; }
    }
}
