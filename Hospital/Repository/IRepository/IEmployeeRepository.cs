using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(long id);
        Task Create(Employee entity);
        Task Update(Employee entity);
        Task Delete(Employee entity);
        Task Save();
        bool IsEmployeeExsits(string email);
    }
}
