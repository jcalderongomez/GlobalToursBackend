using System.Reflection;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pais> Pais {get; set;}
        public DbSet<Categoria> Categoria {get; set;}
        public DbSet<Lugar> Lugar {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //Encargado de crear las migraciones 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}