namespace MedicalAppointmentSystem.Api.Dtos
{
    public class PrescriptionDto
    {
        public string MedicineId { get; set; } = null!;
        public string? Dosage { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
