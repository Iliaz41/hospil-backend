using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface IHealthStatusRepository
    {
        Task<List<HealthStatus>> GetAll();
        Task<HealthStatus> GetById(long id);
        Task Create(HealthStatus entity);
        Task Update(HealthStatus entity);
        Task Delete(HealthStatus entity);
        Task Save();
    }
}
