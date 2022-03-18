using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class SolicitudObra
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Obras")]
        [Required(ErrorMessage = "El campo obra es requerido")]
        public int ObrId { get; set; }

        public virtual Obra Obras { get; set; }

        [ForeignKey("Trabajadores")]
        [Required(ErrorMessage = "El campo trabajador es requerido")]
        public int IdTrabajador { get; set; }

        public virtual Trabajador Trabajadores { get; set; }

        public string Estado { get; set; }
    }
}
