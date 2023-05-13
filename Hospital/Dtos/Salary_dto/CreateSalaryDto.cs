using Hospital.Models;

namespace Hospital.Dtos.Salary_dto
{
    public class CreateSalaryDto
    {
        public int? Kpi { get; set; }

        public long EmployeeId { get; set; }
    }
}
