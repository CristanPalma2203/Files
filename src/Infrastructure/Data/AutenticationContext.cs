

using Domain.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AutenticationContext : DbContext
    {
        public AutenticationContext(DbContextOptions<AutenticationContext> options)
      : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }   

        public DbSet<AppUser> Users { get; set; }
        public DbSet<StoredFile> StoredFiles { get; set; }
		
        protected override void OnModelCreating(ModelBuilder builder)
        {
            EnglishTableConfiguration.Apply(builder);
            base.OnModelCreating(builder);
        }
    }
}
