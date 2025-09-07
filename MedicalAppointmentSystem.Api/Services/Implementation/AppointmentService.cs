using MedicalAppointmentSystem.Api.Data.Context;
using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Dtos.Extension;
using MedicalAppointmentSystem.Api.Models.Entities;
using MedicalAppointmentSystem.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Api.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(AppDbContext context, ILogger<AppointmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            try
            {
                return await _context.Appointments
                    .Include(a => a.Prescriptions)
                        .ThenInclude(p => p.Medicine)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all appointments");
                throw;
            }
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(string id)
        {
            try
            {
                return await _context.Appointments
                    .Include(a => a.Prescriptions)
                        .ThenInclude(p => p.Medicine)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId}", id);
                throw;
            }
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            try
            {
                var appointment = new Appointment
                {
                    PatientId = appointmentDto.PatientId,
                    DoctorId = appointmentDto.DoctorId,
                    AppointmentDate = appointmentDto.AppointmentDate,
                    VisitType = appointmentDto.VisitType,
                    Notes = appointmentDto.Notes,
                    Diagnosis = appointmentDto.Diagnosis,
                    CreatedDate = DateTime.Now,
                };

                _context.Appointments.Add(appointment);

                if (appointmentDto.Prescriptions?.Any() == true)
                {
                    var prescriptions = appointmentDto.Prescriptions.Select(prescriptionDto => new Prescription
                    {
                        AppointmentId = appointment.Id,
                        MedicineId = prescriptionDto.MedicineId,
                        Dosage = prescriptionDto.Dosage,
                        StartDate = prescriptionDto.StartDate,
                        EndDate = prescriptionDto.EndDate,
                        Notes = prescriptionDto.Notes,
                        CreatedDate = DateTime.Now,
                    }).ToList();

                    _context.Prescriptions.AddRange(prescriptions);
                }
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created new appointment with ID {AppointmentId}", appointment.Id);

                return appointment.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                throw;
            }
        }

        public async Task<bool> UpdateAppointmentAsync(string id, CreateAppointmentDto appointmentDto)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return false;
                }

                appointment.PatientId = appointmentDto.PatientId;
                appointment.DoctorId = appointmentDto.DoctorId;
                appointment.AppointmentDate = appointmentDto.AppointmentDate;
                appointment.VisitType = appointmentDto.VisitType;
                appointment.Notes = appointmentDto.Notes;
                appointment.Diagnosis = appointmentDto.Diagnosis;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated appointment with ID {AppointmentId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment with ID {AppointmentId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAppointmentAsync(string id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return false;
                }

                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Deleted appointment with ID {AppointmentId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting appointment with ID {AppointmentId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Prescription>> GetAppointmentPrescriptionsAsync(string appointmentId)
        {
            try
            {
                return await _context.Prescriptions
                    .Include(p => p.Medicine)
                    .Where(p => p.AppointmentId == appointmentId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving prescriptions for appointment {AppointmentId}", appointmentId);
                throw;
            }
        }

        public async Task<bool> AppointmentExistsAsync(string id)
        {
            return await _context.Appointments.AnyAsync(e => e.Id == id);
        }
    }
}
