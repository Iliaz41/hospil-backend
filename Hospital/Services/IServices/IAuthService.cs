using Hospital.Dtos.Employee_dto;

namespace Hospital.Services.IServices
{
    public interface IAuthService
    {
        public Task<string> CreateToken(EmployeeDto employeeDto);
    }
}
