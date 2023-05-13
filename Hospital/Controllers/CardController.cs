using AutoMapper;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Models;
using Hospital.Repository;
using Hospital.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public CardController(ICardRepository cardRepository, IMapper mapper, IPatientRepository patientRepository, IEmployeeRepository employeeRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FullCardDto>>> GetAll()
        {
            var cards = await _cardRepository.GetAll();
            if (cards == null)
            {
                return NoContent();
            }
            var cardsDto = new List<FullCardDto>();
            foreach (var item in cards)
            {
                Employee employee = await _employeeRepository.GetById(item.EmployeeId);
                Patient patient = await _patientRepository.GetById(item.PatientId);
                var element = new FullCardDto();
                element.Id = item.Id;
                element.Diagnosys = item.Diagnosys;
                element.Description = item.Description;
                element.Date_in = item.Date_in;
                element.Date_out = item.Date_out;
                element.EmployeeId = item.EmployeeId;
                element.Employee = employee;
                element.PatientId = item.PatientId;
                element.Patient = patient;
                cardsDto.Add(element);
            }
            return Ok(cardsDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FullCardDto>> GetById(long id)
        {
            var card = await _cardRepository.GetById(id);
            if (card == null)
            {
                return NoContent();
            }
            var fullCardDto = new FullCardDto();
            Employee employee = await _employeeRepository.GetById(card.EmployeeId);
            Patient patient = await _patientRepository.GetById(card.PatientId);
            fullCardDto.Id = card.Id;
            fullCardDto.Diagnosys = card.Diagnosys;
            fullCardDto.Description = card.Description;
            fullCardDto.Date_in = card.Date_in;
            fullCardDto.Date_out = card.Date_out;
            fullCardDto.EmployeeId = card.EmployeeId;
            fullCardDto.Employee = employee;
            fullCardDto.PatientId = card.PatientId;
            fullCardDto.Patient = patient;
            return Ok(fullCardDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCardDto>> Create([FromBody] CreateCardDto cardDto)
        {
            var card = _mapper.Map<Card>(cardDto);
            await _cardRepository.Create(card);
            return CreatedAtAction("GetById", new { id = card.Id }, card);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Card>> Update(long id, [FromBody] CardDto cardDto)
        {
            if (cardDto == null || id != cardDto.Id)
            {
                return BadRequest();
            }
            var card = _mapper.Map<Card>(cardDto);
            await _cardRepository.Update(card);
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
            var card = await _cardRepository.GetById(id);
            if (card == null)
            {
                return NotFound();
            }
            await _cardRepository.Delete(card);
            return NoContent();
        }
    }
}
