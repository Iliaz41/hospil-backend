using AutoMapper;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Models;
using Hospital.Repository;
using Hospital.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public AppointmentController(IAppointmentRepository appointmentRepository, IMapper mapper, IEmployeeRepository employeeRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _patientRepository = patientRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            var appointments = await _appointmentRepository.GetAll();
            if (appointments == null)
            {
                return NoContent();
            }
            var appointmentsDto = new List<FullAppointmentDto>();
            foreach (var item in appointments)
            {
                Employee employee = await _employeeRepository.GetById(item.EmployeeId);
                Patient patient = await _patientRepository.GetById(item.PatientId);
                var element = new FullAppointmentDto();
                element.Id = item.Id;
                element.DateTime = item.DateTime;
                element.Title = item.Title;
                element.Result = item.Result;
                element.EmployeeId = item.EmployeeId;
                element.Employee = employee;
                element.PatientId = item.PatientId;
                element.Patient = patient;
                appointmentsDto.Add(element);
            }
            return Ok(appointmentsDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AppointmentDto>> GetById(long id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return NoContent();
            }
            var appointmentDto = new FullAppointmentDto();
            Employee employee = await _employeeRepository.GetById(appointment.EmployeeId);
            Patient patient = await _patientRepository.GetById(appointment.PatientId);
            appointmentDto.Id = appointment.Id;
            appointmentDto.DateTime = appointment.DateTime;
            appointmentDto.Title = appointment.Title;
            appointmentDto.Result = appointment.Result;
            appointmentDto.EmployeeId = appointment.EmployeeId;
            appointmentDto.Employee = employee;
            appointmentDto.PatientId = appointment.PatientId;
            appointmentDto.Patient = patient;
            return Ok(appointmentDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateAppointmentDto>> Create([FromBody] CreateAppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            await _appointmentRepository.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Appointment>> Update(long id, [FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null || id != appointmentDto.Id)
            {
                return BadRequest();
            }
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            await _appointmentRepository.Update(appointment);
            return NoContent();
        }

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
            var card = await _appointmentRepository.GetById(id);
            if (card == null)
            {
                return NotFound();
            }
            await _appointmentRepository.Delete(card);
            return NoContent();
        }
    }
}
