using Azure.Core;
using MedicalAppointmentSystem.Api.Data.Context;
using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Dtos.Extension;
using MedicalAppointmentSystem.Api.Models.Entities;
using MedicalAppointmentSystem.Api.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedicalAppointmentSystem.Api.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly DatabaseUtility _dbUtility;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(AppDbContext context, DatabaseUtility dbUtility, ILogger<AppointmentService> logger)
        {
            _context = context;
            _dbUtility = dbUtility;
            _logger = logger;            
        }

        public async Task<PagedResult<SearchApointmentsDto>> GetAppointmentsAsync(AppointmentQueryParameters queryParams)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new("@Page", queryParams.Page),
                    new("@PageSize", queryParams.PageSize),
                    new("@SearchTerm", queryParams.SearchTerm ?? (object)DBNull.Value),
                    new("@DoctorId", queryParams.DoctorId ?? (object)DBNull.Value),
                    new("@VisitType", queryParams.VisitType ?? (object)DBNull.Value),
                    new("@SortBy", queryParams.SortBy ?? "AppointmentDate"),
                    new("@SortOrder", queryParams.SortOrder ?? "desc")
                };

                // Execute stored procedure that returns both data and total count
                var dataSet = await _dbUtility.ExecuteStoredProcedureMultipleResultsAsync("sp_GetAppointmentsPaged", parameters);

                var appointments = _dbUtility.ConvertDataTableToList<SearchApointmentsDto>(dataSet.Tables[0]);
                var totalCount = dataSet.Tables[1].Rows.Count > 0 ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalCount"]) : 0;

                var result = new PagedResult<SearchApointmentsDto>
                {
                    Items = appointments,
                    TotalCount = totalCount,
                    Page = queryParams.Page,
                    PageSize = queryParams.PageSize
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments with parameters: {@QueryParams}", queryParams);
                throw;
            }
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(string id)
        {
            try
            {
                var appointment = await _context.Appointments
                    .Where(a => a.Id == id)
                    .Select(a => new AppointmentDto
                    {
                        Id = a.Id,
                        PatientId = a.PatientId,
                        DoctorId = a.DoctorId,
                        AppointmentDate = a.AppointmentDate,
                        VisitType = a.VisitType,
                        Notes = a.Notes,
                        Diagnosis = a.Diagnosis,
                        CreatedDate = a.CreatedDate,

                        Prescriptions = a.Prescriptions.Select(p => new PrescriptionDto
                        {
                            Id = p.Id,
                            MedicineId = p.MedicineId,
                            Dosage = p.Dosage,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Notes = p.Notes,
                        }).ToList(),
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return appointment ?? new AppointmentDto();
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

                if(appointmentDto.Prescriptions?.Any() == true)
                {
                    var existingPrescriptions = _context.Prescriptions.Where(p => p.AppointmentId == id);
                    _context.Prescriptions.RemoveRange(existingPrescriptions);
                    var newPrescriptions = appointmentDto.Prescriptions.Select(prescriptionDto => new Prescription
                    {
                        AppointmentId = appointment.Id,
                        MedicineId = prescriptionDto.MedicineId,
                        Dosage = prescriptionDto.Dosage,
                        StartDate = prescriptionDto.StartDate,
                        EndDate = prescriptionDto.EndDate,
                        Notes = prescriptionDto.Notes,
                        CreatedDate = DateTime.Now,
                    }).ToList();
                    _context.Prescriptions.AddRange(newPrescriptions);
                }

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
