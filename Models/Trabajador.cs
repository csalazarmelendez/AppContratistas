using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class Trabajador
    {
        [Key]
        public int Id { get; set; }

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

        [Required]
        public string CedulaValida { get; set; }

        [Required]
        public string EPSValida { get; set; }

        [Required]
        public string ARLValida { get; set; }

        [Required]
        public string PensionValida { get; set; }

        [Required]
        public string EstadoIngreso { get; set; }

        public string FechaIngresoObra { get; set; }

        public string Observacion { get; set; }

        [ForeignKey("Contratistas")]
        public int IdContratista { get; set; }

        public virtual Contratista Contratistas { get; set; }

        [ForeignKey("Subcontratistas")]
        public int? IdSubcontratista { get; set; }

        public virtual Subcontratista Subcontratistas { get; set; }
    }
}
