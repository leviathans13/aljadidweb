using Microsoft.EntityFrameworkCore;
using SuratApi.Models;

namespace SuratApi.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users => Set<User>();
	public DbSet<Surat> Surat => Set<Surat>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasIndex(u => u.Username).IsUnique();
			entity.Property(u => u.Username).HasMaxLength(100).IsRequired();
			entity.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
			entity.Property(u => u.Role).HasMaxLength(20).IsRequired();
		});

		modelBuilder.Entity<Surat>(entity =>
		{
			entity.HasIndex(s => s.Nomor).IsUnique();
			entity.Property(s => s.Nomor).HasMaxLength(50).IsRequired();
			entity.Property(s => s.Judul).HasMaxLength(200).IsRequired();
			entity.Property(s => s.Isi).HasMaxLength(4000).IsRequired();
			entity.HasOne(s => s.Owner)
				.WithMany(u => u.OwnedSurat)
				.HasForeignKey(s => s.OwnerId)
				.OnDelete(DeleteBehavior.Cascade);
		});
	}
}

