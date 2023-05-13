using Hospital.Models;

namespace Hospital.Dtos.Appointment_dto
{
    public class FullAppointmentDto
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Title { get; set; }

        public string? Result { get; set; }

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public long PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
