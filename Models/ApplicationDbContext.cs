using Microsoft.EntityFrameworkCore;

namespace SuratMenyuratAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Surat> Surats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                
                entity.Property(e => e.Role)
                    .HasDefaultValue("User");
            });

            // Configure Surat entity
            modelBuilder.Entity<Surat>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.NomorSurat).IsUnique();
                
                entity.HasOne(e => e.CreatedBy)
                    .WithMany(u => u.SuratDibuat)
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.AssignedTo)
                    .WithMany(u => u.SuratDiterima)
                    .HasForeignKey(e => e.AssignedToId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@surat.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    FullName = "Administrator",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    Email = "user1@surat.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "User",
                    FullName = "User Pertama",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}