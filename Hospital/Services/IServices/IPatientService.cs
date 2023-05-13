using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.HealthStatus_dto;
using Hospital.Dtos.Patient_dto;

namespace Hospital.Services.IServices
{
    public interface IPatientService
    {
        Task<List<CardDto>> GetCards(long id);
        Task<List<AppointmentDto>> GetAppointments(long id);
        Task<List<HealthStatusDto>> GetHealthStatuses(long id);
        Task<EmployeeDto> GetEmployee(long id);
        Task<List<EmployeeDto>> GetAllEmployees(long id);
        Task<List<PatientDto>> GetByName(string name);
        Task<List<PatientDto>> GetBySurname(string surname);
        Task<List<PatientDto>> GetByPassport(string passport);
        Task<List<PatientDto>> GetByBirthday(DateTime date);
        Task<List<PatientDto>> GetByRoom(int room);
    }
}
