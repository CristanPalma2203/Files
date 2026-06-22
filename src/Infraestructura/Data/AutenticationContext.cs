

using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data
{
    public class AutenticationContext : DbContext
    {
        public AutenticationContext(DbContextOptions<AutenticationContext> options)
      : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }   

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Archivo> archivo { get; set; }
		
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
