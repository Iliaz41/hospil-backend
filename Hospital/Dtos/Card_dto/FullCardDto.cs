using Hospital.Models;

namespace Hospital.Dtos.Card_dto
{
    public class FullCardDto
    {
        public long Id { get; set; }

        public string? Diagnosys { get; set; }

        public string? Description { get; set; }

        public DateTime? Date_in { get; set; }

        public DateTime? Date_out { get; set; }

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public long PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
