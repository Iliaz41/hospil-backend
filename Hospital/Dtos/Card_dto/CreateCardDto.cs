using Hospital.Models;

namespace Hospital.Dtos.Card_dto
{
    public class CreateCardDto
    {
        public string? Diagnosys { get; set; }

        public string? Description { get; set; }

        public DateTime? Date_in { get; set; }

        public DateTime? Date_out { get; set; }

        public long EmployeeId { get; set; }

        public long PatientId { get; set; }
    }
}
