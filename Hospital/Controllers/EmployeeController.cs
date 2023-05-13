using AutoMapper;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Dtos.Photo_dto;
using Hospital.Dtos.Salary_dto;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Hospital.Services;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IMinioService _minioService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IEmployeeService employeeService, IMinioService minioService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeService = employeeService;
            _minioService = minioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeService.GetAll();
            if (employees == null)
            {
                return NoContent();
            }
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDto>> GetById(long id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NoContent();
            }
            return Ok(employee);
        }

        [HttpGet("{id:long}/Patients")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients(long id)
        {
            var patients = await _employeeService.GetPatients(id);
            if (patients == null)
            {
                return NoContent();
            }
            return Ok(patients);
        }

        [HttpGet("{id:long}/Cards")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(long id)
        {
            var cards = await _employeeService.GetCards(id);
            if (cards == null)
            {
                return NoContent();
            }
            return Ok(cards);
        }

        [HttpGet("{id:long}/Appointments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments(long id)
        {
            var appontments = await _employeeService.GetAppointments(id);
            if (appontments == null)
            {
                return NoContent();
            }
            return Ok(appontments);
        }

        [HttpGet("{id:long}/Kpi")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalaryDto>>> GetKpis(long id)
        {
            var salary = await _employeeService.GetKpis(id);
            if (salary == null)
            {
                return NoContent();
            }
            return Ok(salary);
        }

        [HttpGet]
        [Route("EmployeeByLoginTime")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByLoginTime([FromQuery] int amount)
        {
            var items = await _employeeService.GetByLoginTime(amount);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("EmployeeByKpi")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByKpi([FromQuery] int kpi)
        {
            var items = await _employeeService.GetEmployeeByKpi(kpi);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("EmployeeBySurname")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetBySurname([FromQuery] string surname)
        {
            var items = await _employeeService.GetBySurname(surname);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("EmployeeByName")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByName([FromQuery] string name)
        {
            var items = await _employeeService.GetByName(name);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("EmployeeByEmail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByEmail([FromQuery] string email)
        {
            var items = await _employeeService.GetByEmail(email);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("EmployeeByPassport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByPassport([FromQuery] string passport)
        {
            var items = await _employeeService.GetByPassport(passport);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateEmployeeDto>> Create([FromBody] CreateEmployeeDto employeeDto)
        {
            var dto = _mapper.Map<Employee>(employeeDto);
            await _employeeService.Create(dto);
            return CreatedAtAction("GetById", new { id = dto.Id }, dto);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Update(long id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null || id != employeeDto.Id)
            {
                return BadRequest();
            }
            //var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeService.Update(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(long id)
        {
            await _employeeService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Upload([FromForm] IFormPhotoDto iformPhotoDto)
        {
            var photo = _mapper.Map<StrPhotoDto>(iformPhotoDto);
            var str = await _minioService.UploadFile(photo.Photo, photo.Id);
            return NoContent();
        }
    }
}
