using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Card
    {
        [Key]
        public long Id { get; set; }

        public string? Diagnosys { get; set; }

        public string? Description { get; set; }

        public DateTime? Date_in { get; set; }

        public DateTime? Date_out { get; set; }

        public long EmployeeId { get; set; }

        public long PatientId { get; set; }
    }
}
