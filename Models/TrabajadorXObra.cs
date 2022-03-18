using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Models
{
    public class TrabajadorXObra
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Obras")]
        public int ObrId{ get; set; }

        public virtual Obra Obras { get; set; }

        [ForeignKey("Trabajadores")]
        public int IdTrabajador { get; set; }

        public virtual Trabajador Trabajadores { get; set; }

        [ForeignKey("SolicitudObras")]
        public int? IdSolicitudObra { get; set; }

        public virtual SolicitudObra SolicitudObras { get; set; }
    }
}
