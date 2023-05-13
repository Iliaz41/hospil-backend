using Hospital.Models;

namespace Hospital.Dtos.Salary_dto
{
    public class SalaryDto
    {
        public long Id { get; set; }

        public int? Kpi { get; set; }

        public long EmployeeId { get; set; }
    }
}
