using MedicalAppointmentSystem.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Api.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Medicine)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments) 
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding sample data
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = "1", DoctorName = "Dr. John Smith", Specialization = "Cardiologist" },
                new Doctor { Id = "2", DoctorName = "Dr. Emily Brown", Specialization = "Dermatologist" }, 
                new Doctor { Id = "3", DoctorName = "Dr. Bell Smith", Specialization = "Cardiologist" },
                new Doctor { Id = "4", DoctorName = "Dr. Hars Brown", Specialization = "Dermatologist" }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = "1", PatientName = "Alice Johnson", PatientEmail = "Sadek.gtrbd@gmail.com" },
                new Patient { Id = "2", PatientName = "Emile Johnson", PatientEmail = "Sadek.gtrbd@gmail.com" },
                new Patient {Id = "3", PatientName = "Sarah Davis", PatientEmail = "Sadek.gtrbd@gmail.com" },
                new Patient {Id = "4", PatientName = "John Doe", PatientEmail = "Sadek.gtrbd@gmail.com" }
            );

            modelBuilder.Entity<Medicine>().HasData(
                new Medicine { Id = "1", MedicineName = "Paracetamol", Description = "Pain reliever and fever reducer" },
                new Medicine { Id = "2", MedicineName = "Amoxicillin", Description = "Antibiotic used to treat infections" },
                new Medicine { Id = "3", MedicineName = "Ibuprofen", Description = "Anti-inflammatory and pain relief" },
                new Medicine { Id = "4", MedicineName = "Cetirizine", Description = "Antihistamine for allergies" }
            );
        }
    }
}
