using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class HistorialFechasPlanilla
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Trabajadores")]
        public int IdTrabajador { get; set; }

        public virtual Trabajador Trabajadores { get; set; }

        [Required]
        public string Fecha { get; set; }
    }
}
