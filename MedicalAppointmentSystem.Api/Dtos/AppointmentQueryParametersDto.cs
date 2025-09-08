using MedicalAppointmentSystem.Api.Models.Enums;

namespace MedicalAppointmentSystem.Api.Dtos
{
    public class AppointmentQueryParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SearchTerm { get; set; }
        // Filtering parameters
        public string? DoctorId { get; set; }
        public VisitTypeEnum? VisitType { get; set; }

        public string? SortBy { get; set; } = "AppointmentDate";
        public string? SortOrder { get; set; } = "desc";
    }
}
