using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ITI.HMS.Models
{
    public class HMSDbContext : DbContext
    {
        public HMSDbContext(DbContextOptions<HMSDbContext> options) : base(options)
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User-Doctor relationship
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique constraint on Doctor.UserId (one user can only be one doctor)
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.UserId)
                .IsUnique();

            // Configure User-Patient relationship
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique constraint on Patient.UserId (one user can only be one patient)
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.UserId)
                .IsUnique();

            // Configure User-RefreshToken relationship
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Create index on Token for faster lookups
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
