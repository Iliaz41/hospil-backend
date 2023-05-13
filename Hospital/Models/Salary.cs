using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Salary
    {
        [Key]
        public long Id { get; set; }

        public int? Kpi { get; set; }

        public long EmployeeId { get; set; }
    }
}
