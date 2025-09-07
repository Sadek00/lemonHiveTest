using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentSystem.Api.Dtos
{
    public class CreatePrescriptionDto
    {
        [Required]
        public required string AppointmentId { get; set; }

        [Required]
        public required string MedicineId { get; set; }

        public string? Dosage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
    public class CreatePrescriptionForAppointmentDto
    {
        [Required]
        public required string MedicineId { get; set; }

        public string? Dosage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
