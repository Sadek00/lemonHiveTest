namespace MedicalAppointmentSystem.Api.Dtos
{
    public class SearchApointmentsDto
    {
        public string? Id { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int VisitType { get; set; }
        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
