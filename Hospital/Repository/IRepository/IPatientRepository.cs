using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAll();
        Task<Patient> GetById(long id);
        Task Create(Patient entity);
        Task Update(Patient entity);
        Task Delete(Patient entity);
        Task Save();
        bool IsPatientExsits(string passport);
    }
}
