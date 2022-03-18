using Contratistas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Operador> Operador { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Contratista> Contratista { get; set; }
        public DbSet<Subcontratista> Subcontratista { get; set; }
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<SolicitudRegistro> SolicitudRegistro { get; set; }
        public DbSet<Obra> Obra { get; set; }
        public DbSet<TrabajadorXObra> TrabajadorXObra { get; set; }
        public DbSet<SolicitudObra> SolicitudObra { get; set; }
    }
}
