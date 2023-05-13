using AutoMapper;
using Hospital.Dtos.Employee_dto;
using Hospital.Models;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using Xceed.Document.NET;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IEmployeeService employeeService, IAuthService authService, IMapper mapper) 
        {
            _employeeService = employeeService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<CreateEmployeeDto>> Registration([FromBody] CreateEmployeeDto employeeDto)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(employeeDto.Password);

            CreateEmployeeDto responce = employeeDto;

            employeeDto.Password = passwordHash;
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeService.Create(employee);

            return Ok(responce);
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<EmployeeDto>> Login(EmployeeDto employeeDto)
        {
            var employee = await _employeeService.GetById(employeeDto.Id);
            if (employee.Email != employee.Email)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(employeeDto.Password, employee.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = await _authService.CreateToken(employeeDto);

            return Ok(token);
        }

    }
}
