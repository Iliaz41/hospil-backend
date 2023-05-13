using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Dtos.Salary_dto;
using Hospital.Models;

namespace Hospital.Services.IServices
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetBySurname(string surname);
        Task<List<EmployeeDto>> GetByName(string name);
        Task<List<EmployeeDto>> GetByEmail(string email);
        Task<List<EmployeeDto>> GetByPassport(string passport);
        Task<List<EmployeeDto>> GetByLoginTime(int amount);
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(long id);
        Task Create(Employee entity);
        Task Update(EmployeeDto entity);
        Task Delete(long id);
        Task<List<PatientDto>> GetPatients(long id);
        Task<List<EmployeeDto>> GetEmployeeByKpi(int kpi);
        Task<SalaryDto> GetKpis(long id);
        Task<List<CardDto>> GetCards(long id);
        Task<List<AppointmentDto>> GetAppointments(long id);
    }
}
