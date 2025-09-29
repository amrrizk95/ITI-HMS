using ITI.HMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ITI.HMS.Models
{
    public class HMSDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<MedicalRecordDetails> MedicalRecordDetails { get; set; }

        public HMSDbContext(DbContextOptions<HMSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.ToTable("Doctors");

                entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(d => d.Specialty)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(d => d.Phone)
                .IsRequired()
                .HasMaxLength(11);

                entity.HasMany(d => d.Appointments)
                      .WithOne(a => a.Doctor)
                      .HasForeignKey(a => a.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.MedicalRecords)
                      .WithOne(mr => mr.Doctor)
                      .HasForeignKey(mr => mr.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.ToTable("Patients");

                entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

                entity.HasMany(p => p.Appointments)
                      .WithOne(a => a.Patient)
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.MedicalRecords)
                      .WithOne(mr => mr.Patient)
                      .HasForeignKey(mr => mr.PatientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => new {a.DoctorId , a.PatientId});

                entity.ToTable("Appointments");

                entity.Property(a => a.Status)
                .HasConversion<string>();
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(mr => mr.Id);

                entity.ToTable("MedicalRecords");

                entity.HasMany(mr => mr.Details)
                      .WithOne(mrd => mrd.MedicalRecord)
                      .HasForeignKey(mrd => mrd.MedicalRecordId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MedicalRecordDetails>(entity =>
            {
                entity.HasKey(mrd => mrd.Id);

                entity.ToTable("MedicalRecordDetails");

                entity.Property(mrd => mrd.Type)
                .HasConversion<string>();
            });
        }
    }
}