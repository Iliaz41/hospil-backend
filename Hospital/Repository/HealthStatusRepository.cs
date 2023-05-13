using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository
{
    public class HealthStatusRepository : IHealthStatusRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public HealthStatusRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<List<HealthStatus>> GetAll()
        {
            List<HealthStatus> healthStatuses = await _dbContext.HealthStatuses.ToListAsync();
            return healthStatuses;
        }
        public async Task<HealthStatus> GetById(long id)
        {
            HealthStatus healthStatus = await _dbContext.HealthStatuses.FindAsync(id);
            return healthStatus;
        }
        public async Task Create(HealthStatus entity)
        {
            await _dbContext.HealthStatuses.AddAsync(entity);
            await Save();
        }
        public async Task Update(HealthStatus entity)
        {
            _dbContext.HealthStatuses.Update(entity);
            await Save();
        }
        public async Task Delete(HealthStatus entity)
        {
            _dbContext.HealthStatuses.Remove(entity);
            await Save();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
