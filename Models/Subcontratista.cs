using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class Subcontratista
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
    }
}
