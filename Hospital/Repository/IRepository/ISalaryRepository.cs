using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface ISalaryRepository
    {
        Task<List<Salary>> GetAll();
        Task<Salary> GetById(long id);
        Task Create(Salary entity);
        Task Update(Salary entity);
        Task Delete(Salary entity);
        Task Save();
    }
}
