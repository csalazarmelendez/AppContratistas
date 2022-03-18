using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cedula { get; set; }

        [Required]
        public string EPS { get; set; }

        [Required]
        public string ARL { get; set; }

        [Required]
        public string Pension { get; set; }

        [ForeignKey("Trabajadores")]
        public int Trabajador { get; set; }

        public virtual Trabajador Trabajadores { get; set; }
    }
}
