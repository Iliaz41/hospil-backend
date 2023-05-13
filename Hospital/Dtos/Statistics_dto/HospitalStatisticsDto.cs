using System.Security.Cryptography.X509Certificates;

namespace Hospital.Dtos.Statistics_dto
{
    public class HospitalStatisticsDto
    {
        public int AmountsEmployees { get; set; }
        public int AmountsCards { get; set; }
        public int AmountPatients { get; set; }
        public int AmountAppointments { get; set; }
        public float? AverageIndex { get; set; }
        //public float? AverageSalary { get; set; }
    }
}
