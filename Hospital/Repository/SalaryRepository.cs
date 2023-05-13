using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SalaryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Salary>> GetAll()
        {
            List<Salary> salaries = await _dbContext.Salaries.ToListAsync();
            return salaries;
        }
        public async Task<Salary> GetById(long id)
        {
            Salary salary = await _dbContext.Salaries.FindAsync(id);
            return salary;
        }
        public async Task Create(Salary entity)
        {
            await _dbContext.Salaries.AddAsync(entity);
            await Save();
        }
        public async Task Update(Salary entity)
        {
            _dbContext.Salaries.Update(entity);
            await Save();
        }
        public async Task Delete(Salary entity)
        {
            _dbContext.Salaries.Remove(entity);
            await Save();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
