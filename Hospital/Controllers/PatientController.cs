using AutoMapper;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.HealthStatus_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Hospital.Services;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        public PatientController(IPatientRepository patientRepository, IMapper mapper, IPatientService patientService)
        {
            _patientRepository = patientRepository;
            _patientService = patientService;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await _patientRepository.GetAll();
            var patientsDto = _mapper.Map<List<PatientDto>>(patients);
            if (patients == null)
            {
                return NoContent();
            }
            return Ok(patientsDto);
        }
        
        [Authorize]
        [HttpGet("{id:long}/Cards")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(long id)
        {
            var cardDtos = await _patientService.GetCards(id);
            if (cardDtos == null)
            {
                return NoContent();
            }
            return Ok(cardDtos);
        }
        
        [Authorize]
        [HttpGet("{id:long}/Appointments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments(long id)
        {
            var appointmentDtos = await _patientService.GetAppointments(id);
            if (appointmentDtos == null)
            {
                return NoContent();
            }
            return Ok(appointmentDtos);
        }
        
        [Authorize]
        [HttpGet("{id:long}/HealthStatuses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HealthStatusDto>>> GetHealthStatuses(long id)
        {
            var statusDtos = await _patientService.GetHealthStatuses(id);
            if (statusDtos == null)
            {
                return NoContent();
            }
            return Ok(statusDtos);
        }
        
        [Authorize]
        [HttpGet("{id:long}/Employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(long id)
        {
            var employeeDto = await _patientService.GetEmployee(id);
            if (employeeDto == null)
            {
                return NoContent();
            }
            return Ok(employeeDto);
        }
        
        [Authorize]
        [HttpGet("{id:long}/Employees")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees(long id)
        {
            var employeeDtos = await _patientService.GetAllEmployees(id);
            if (employeeDtos == null)
            {
                return NoContent();
            }
            return Ok(employeeDtos);
        }
        
        [Authorize]
        [HttpGet]
        [Route("PatientByName")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetByName([FromQuery] string name)
        {
            var items = await _patientService.GetByName(name);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }
        
        [Authorize]
        [HttpGet]
        [Route("PatientBySurame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetBySurname([FromQuery] string surname)
        {
            var items = await _patientService.GetBySurname(surname);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }
        
        [Authorize]
        [HttpGet]
        [Route("PatientByPassport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetByPassport([FromQuery] string passport)
        {
            var items = await _patientService.GetByPassport(passport);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }
        
        [Authorize]
        [HttpGet]
        [Route("PatientByBirthday")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetByBirthday([FromQuery] DateTime date)
        {
            var items = await _patientService.GetByBirthday(date);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }
        
        [Authorize]
        [HttpGet]
        [Route("PatientByRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetByRoom([FromQuery] int room)
        {
            var items = await _patientService.GetByRoom(room);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }
        
        [Authorize]
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PatientDto>> GetById(long id)
        {
            var patient = await _patientRepository.GetById(id);
            var patientDto = _mapper.Map<PatientDto>(patient);
            if (patient == null)
            {
                return NoContent();
            }
            return Ok(patientDto);
        }
        
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreatePatientDto>> Create([FromBody] CreatePatientDto patientDto)
        {
            var result = _patientRepository.IsPatientExsits(patientDto.Passport);
            if (result)
            {
                return Conflict("States already exsits in databse");
            }
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientRepository.Create(patient);
            return CreatedAtAction("GetById", new { id = patient.Id }, patient);
        }
        
        [Authorize]
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Patient>> Update(long id, [FromBody] PatientDto patientDto)
        {
            if (patientDto == null || id != patientDto.Id)
            {
                return BadRequest();
            }
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientRepository.Update(patient);
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
            var patient = await _patientRepository.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            await _patientRepository.Delete(patient);
            return NoContent();
        }
    }
}
