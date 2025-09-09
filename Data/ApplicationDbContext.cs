using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuratMenyuratAPI.Models;

namespace SuratMenyuratAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Letter> Letters { get; set; }
        public DbSet<LetterAttachment> LetterAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Letter entity
            builder.Entity<Letter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.LetterNumber).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.LetterNumber).IsUnique();

                // Configure relationships
                entity.HasOne(e => e.Sender)
                    .WithMany(u => u.SentLetters)
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Recipient)
                    .WithMany(u => u.ReceivedLetters)
                    .HasForeignKey(e => e.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure LetterAttachment entity
            builder.Entity<LetterAttachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FilePath).IsRequired().HasMaxLength(255);
                entity.Property(e => e.ContentType).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Letter)
                    .WithMany(l => l.Attachments)
                    .HasForeignKey(e => e.LetterId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure User entity
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
            });

            // Seed roles
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            var adminRoleId = "1";
            var userRoleId = "2";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}