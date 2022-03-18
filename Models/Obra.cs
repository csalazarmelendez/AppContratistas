using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class Obra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int obrId { get; set; }

        [Required]
        public string obrCodigo { get; set; }

        [Required]
        public int pryId { get; set; }

        [Required]
        public string obrNombre { get; set; }
    }
}
