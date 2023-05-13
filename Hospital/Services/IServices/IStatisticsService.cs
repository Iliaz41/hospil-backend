using Hospital.Dtos.Statistics_dto;
using System.Runtime.CompilerServices;

namespace Hospital.Services.IServices
{
    public interface IStatisticsService
    {
        public Task<PatientStatisticsDto> GetPatientStatistics(long id);
        public Task<EmployeeStatisticsDto> GetEmployeeStatistics(long id);
        public Task<HospitalStatisticsDto> GetHospitalStatistics();
    }
}
