using AutoMapper;
using Hospital.Dtos.Salary_dto;
using Hospital.Models;
using Hospital.Repository;
using Hospital.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : Controller
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public SalaryController(ISalaryRepository salaryRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalaryDto>>> GetAll()
        {
            var salaries = await _salaryRepository.GetAll();
            if (salaries == null)
            {
                return NoContent();
            }
            var salariesDto = new List<FullSalaryDto>();
            foreach(var item in salaries)
            {
                Employee employee = await _employeeRepository.GetById(item.EmployeeId);
                var elem = new FullSalaryDto();
                elem.Id = item.Id;
                elem.Kpi = item.Kpi;
                elem.EmployeeId = item.EmployeeId;
                elem.Employee = employee;
                salariesDto.Add(elem);
            }
            return Ok(salariesDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SalaryDto>> GetById(long id)
        {
            var salary = await _salaryRepository.GetById(id);
            var salaryDto = _mapper.Map<SalaryDto>(salary);
            if (salary == null)
            {
                return NoContent();
            }
            return Ok(salaryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateSalaryDto>> Create([FromBody] CreateSalaryDto salaryDto)
        {
            var salary = _mapper.Map<Salary>(salaryDto);
            await _salaryRepository.Create(salary);
            return CreatedAtAction("GetById", new { id = salary.Id }, salary);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Salary>> Update(long id, [FromBody] SalaryDto salaryDto)
        {
            if (salaryDto == null || id != salaryDto.Id)
            {
                return BadRequest();
            }
            var salary = _mapper.Map<Salary>(salaryDto);
            await _salaryRepository.Update(salary);
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
            var salary = await _salaryRepository.GetById(id);
            if (salary == null)
            {
                return NotFound();
            }
            await _salaryRepository.Delete(salary);
            return NoContent();
        }
    }
}
