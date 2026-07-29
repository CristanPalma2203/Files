using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public static class EnglishTableConfiguration
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<StoredFile>().ToTable("stored_file");
            builder.Entity<AppUser>().ToTable("app_user");

            // Navigation `appUser` would otherwise create shadow FK appUserId.
            builder.Entity<StoredFile>()
                .HasOne(f => f.appUser)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
