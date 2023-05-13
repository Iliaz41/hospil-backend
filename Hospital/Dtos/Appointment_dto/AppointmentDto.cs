using Hospital.Models;

namespace Hospital.Dtos.Appointment_dto
{
    public class AppointmentDto
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Title { get; set; }

        public string? Result { get; set; }

        public long EmployeeId { get; set; }

        public long PatientId { get; set; }
    }
}
