using AutoMapper;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.HealthStatus_dto;
using Hospital.Models;
using Hospital.Repository;
using Hospital.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStatusController : Controller
    {
        private readonly IHealthStatusRepository _healthStatusRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public HealthStatusController(IHealthStatusRepository healthStatusRepository, IMapper mapper, IPatientRepository patientRepository)
        {
            _healthStatusRepository = healthStatusRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HealthStatusDto>>> GetAll()
        {
            var statuses = await _healthStatusRepository.GetAll();
            if (statuses == null)
            {
                return NoContent();
            }
            var statusesDto = new List<FullHealthStatusDto>();
            foreach (var item in statuses)
            {
                Patient patient = await _patientRepository.GetById(item.PatientId);
                var element = new FullHealthStatusDto();
                element.Id = item.Id;
                element.Date = item.Date;
                element.Description = item.Description;
                element.Temperature = item.Temperature;
                element.PressureSyst = item.PressureSyst;
                element.PressureDiast = item.PressureDiast;
                element.Pulse = item.Pulse;
                element.Index = item.Index;
                element.PatientId = item.PatientId;
                element.Patient = patient;
                statusesDto.Add(element);
            }
            return Ok(statusesDto);
        }
        
        [Authorize]
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HealthStatusDto>> GetById(long id)
        {
            var status = await _healthStatusRepository.GetById(id);
            if (status == null)
            {
                return NoContent();
            }
            var statusDto = new FullHealthStatusDto();
            Patient patient = await _patientRepository.GetById(status.PatientId);
            statusDto.Id = status.Id;
            statusDto.Date = status.Date;
            statusDto.Description = status.Description;
            statusDto.Temperature = status.Temperature;
            statusDto.PressureSyst = status.PressureSyst;
            statusDto.PressureDiast = status.PressureDiast;
            statusDto.Pulse = status.Pulse;
            statusDto.Index = status.Index;
            statusDto.PatientId = status.PatientId;
            statusDto.Patient = patient;
            return Ok(statusDto);
        }
        
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateHealthStatusDto>> Create([FromBody] CreateHealthStatusDto healthStatusDto)
        {
            var status = _mapper.Map<HealthStatus>(healthStatusDto);
            await _healthStatusRepository.Create(status);
            return CreatedAtAction("GetById", new { id = status.Id }, status);
        }
        
        [Authorize]
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HealthStatus>> Update(long id, [FromBody] HealthStatusDto healthStatusDto)
        {
            if (healthStatusDto == null || id != healthStatusDto.Id)
            {
                return BadRequest();
            }
            var status = _mapper.Map<HealthStatus>(healthStatusDto);
            await _healthStatusRepository.Update(status);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var status = await _healthStatusRepository.GetById(id);
            if (status == null)
            {
                return NotFound();
            }
            await _healthStatusRepository.Delete(status);
            return NoContent();
        }
    }
}
