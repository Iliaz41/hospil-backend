using Hospital.Models;

namespace Hospital.Dtos.Patient_dto
{
    public class PatientDto
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Passport { get; set; }
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public string? Flat { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Room { get; set; }

        public long? Phone { get; set; }
    }
}
