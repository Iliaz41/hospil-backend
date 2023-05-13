using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Hospital.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppointmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Appointment>> GetAll()
        {
            List<Appointment> appointments = await _dbContext.Appointments.ToListAsync();
            return appointments;
        }
        public async Task<Appointment> GetById(long id)
        {
            Appointment appointment = await _dbContext.Appointments.FindAsync(id);
            return appointment;
        }
        public async Task Create(Appointment entity)
        {
            await _dbContext.Appointments.AddAsync(entity);
            await Save();
        }
        public async Task Update(Appointment entity)
        {
            _dbContext.Appointments.Update(entity);
            await Save();
        }
        public async Task Delete(Appointment entity)
        {
            _dbContext.Appointments.Remove(entity);
            await Save();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
