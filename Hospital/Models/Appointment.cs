using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Appointment
    {
        [Key]
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Title { get; set; }

        public string? Result { get; set; }

        public long EmployeeId { get; set; }

        public long PatientId { get; set; }
    }
}
