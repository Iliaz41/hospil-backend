using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task Create(Patient entity)
        {
            await _dbContext.Patients.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Patient entity)
        {
            _dbContext.Patients.Remove(entity);
            await Save();
        }

        public async Task<List<Patient>> GetAll()
        {
            List<Patient> patients = await _dbContext.Patients.ToListAsync();
            return patients;
        }

        public async Task<Patient> GetById(long id)
        {
            Patient patient = await _dbContext.Patients.FindAsync(id);
            return patient;
        }

        public bool IsPatientExsits(string passport)
        {
            var result = _dbContext.Patients.AsQueryable().Where(x => x.Passport.ToLower().Trim() == passport.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Patient entity)
        {
            _dbContext.Patients.Update(entity);
            await Save();
        }
    }
}
