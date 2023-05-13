namespace Hospital.Dtos.Statistics_dto
{
    public class EmployeeStatisticsDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public int AmountHours { get; set; }
        public int AmountAppointments { get; set; }
        public int AmountCards { get; set; }
    }
}
