using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAll();
        Task<Appointment> GetById(long id);
        Task Create(Appointment entity);
        Task Update(Appointment entity);
        Task Delete(Appointment entity);
        Task Save();
    }
}
