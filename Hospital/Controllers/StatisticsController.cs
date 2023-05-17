using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.Statistics_dto;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService) 
        {
            _statisticsService = statisticsService;
        }
        
        [Authorize]
        [HttpGet("PatientsStatistics/{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PatientStatisticsDto>> GetPatientStatistics(long id)
        {
            var stat = await _statisticsService.GetPatientStatistics(id);
            if (stat == null)
            {
                return NoContent();
            }
            return Ok(stat);
        }
        
        [Authorize]
        [HttpGet("EmployeesStatistics/{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeStatisticsDto>> GetEmployeeStatistics(long id)
        {
            var stat = await _statisticsService.GetEmployeeStatistics(id);
            if (stat == null)
            {
                return NoContent();
            }
            return Ok(stat);
        }
        
        [Authorize]
        [HttpGet("HospitalsStatistics")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HospitalStatisticsDto>> GetHospitalStatistics()
        {
            var stat = await _statisticsService.GetHospitalStatistics();
            if (stat == null)
            {
                return NoContent();
            }
            return Ok(stat);
        }
    }
}
