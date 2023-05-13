using Hospital.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HealthStatus> HealthStatuses { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
}
