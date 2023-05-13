using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Employee entity)
        {
            await _dbContext.Employees.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            await Save();   
        }

        public async Task<List<Employee>> GetAll()
        {
            List<Employee> employees = await _dbContext.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(long id)
        {
            Employee employee = await _dbContext.Employees.FindAsync(id);
            return employee;
        }

        public bool IsEmployeeExsits(string email)
        {
            var result = _dbContext.Employees.AsQueryable().Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            await Save();
        }
    }
}
