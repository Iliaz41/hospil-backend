namespace Hospital.Dtos.Statistics_dto
{
    public class PatientStatisticsDto
    {
        public long PatientId { get; set; }
        public string PatientName { get; set;}
        public string PatientSurname { get; set;}
        public float AverageIndex { get; set; }
        public int Days { get; set; }
        public int Appointments { get; set; }
    }
}
