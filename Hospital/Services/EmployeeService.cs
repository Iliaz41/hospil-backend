using AutoMapper;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Dtos.Salary_dto;
using Hospital.Models;
using Hospital.Repository;
using Hospital.Repository.IRepository;
using Hospital.Services.IServices;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;

namespace Hospital.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ICardRepository cardRepository, IPatientRepository patientRepository, ISalaryRepository salaryRepository, IAppointmentRepository appointmentRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _cardRepository = cardRepository;
            _patientRepository = patientRepository;
            _salaryRepository = salaryRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<List<EmployeeDto>> GetBySurname (string surname)
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees) 
            {
                if(employee.Surname == surname)
                {
                    var element = _mapper.Map<EmployeeDto>(employee);
                    employeeDtos.Add(element);
                }
            }
            return employeeDtos;
        }
        public async Task<List<EmployeeDto>> GetByName (string name)
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                if (employee.Name == name)
                {
                    var element = _mapper.Map<EmployeeDto>(employee);
                    employeeDtos.Add(element);
                }
            }
            return employeeDtos;
        }
        public async Task<List<EmployeeDto>> GetByEmail(string email)
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                if (employee.Email == email)
                {
                    var element = _mapper.Map<EmployeeDto>(employee);
                    employeeDtos.Add(element);
                }
            }
            return employeeDtos;
        }
        public async Task<List<EmployeeDto>> GetByPassport(string passport)
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                if (employee.Passport == passport)
                {
                    var element = _mapper.Map<EmployeeDto>(employee);
                    employeeDtos.Add(element);
                }
            }
            return employeeDtos;
        }
        public async Task<List<EmployeeDto>> GetByLoginTime(int amount)
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                if (employee.LoginTime == amount)
                {
                    var element = _mapper.Map<EmployeeDto>(employee);
                    employeeDtos.Add(element);
                }
            }
            return employeeDtos;
        }
        public async Task<List<EmployeeDto>> GetAll()
        {
            List<Employee> employees = await _employeeRepository.GetAll();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                var element = _mapper.Map<EmployeeDto>(employee);
                employeeDtos.Add(element);
            }
            return employeeDtos;
        }
        public async Task<EmployeeDto> GetById(long id)
        {
            Employee employee = await _employeeRepository.GetById(id);
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public async Task Create(Employee entity)
        {
            var employee = _mapper.Map<Employee>(entity);
            var result = _employeeRepository.IsEmployeeExsits(employee.Email);
            if (result)
            {
                throw new ArgumentException("Employee is already exsits");
            }
            await _employeeRepository.Create(employee);
            await _employeeRepository.Save();
        }
        public async Task Update(EmployeeDto entity) 
        {
            var employee = _mapper.Map<Employee>(entity);
            await _employeeRepository.Update(employee);
            await _employeeRepository.Save();
        }
        public async Task Delete(long id)
        {
            Employee employee = await _employeeRepository.GetById(id);
            await _employeeRepository.Delete(employee);
            await _employeeRepository.Save();
        }
        public async Task<List<PatientDto>> GetPatients(long id)
        {
            var cards = await _cardRepository.GetAll();
            var cardsWithId = new List<Card>();
            foreach(var item in cards)
            {
                if(item.EmployeeId == id)
                {
                    cardsWithId.Add(item);
                }
            }
            List<PatientDto> patientsDto = new List<PatientDto>();
            foreach (var elem in cardsWithId)
            {
                Patient patient = await _patientRepository.GetById(elem.PatientId);
                patientsDto.Add(_mapper.Map<PatientDto>(patient));
            }
            return patientsDto;
        }
        public async Task<List<EmployeeDto>> GetEmployeeByKpi(int kpi)
        {
            var elements = await _salaryRepository.GetAll();
            var elementsWithKpi = new List<Salary>();
            foreach(var item in elements)
            {
                if(item.Kpi == kpi)
                {
                    elementsWithKpi.Add(item);
                }
            }
            List<EmployeeDto> employeesDto = new List<EmployeeDto>();
            foreach (var elem in elementsWithKpi)
            {
                Employee employee = await _employeeRepository.GetById(elem.EmployeeId);
                employeesDto.Add(_mapper.Map<EmployeeDto>(employee));
            }
            return employeesDto;
        }
        public async Task<SalaryDto> GetKpis(long id)
        {
            var salaries = await _salaryRepository.GetAll();
            var salaryDto = new SalaryDto();
            foreach(var item in salaries)
            {
                if(item.EmployeeId == id)
                {
                    salaryDto = _mapper.Map<SalaryDto>(item);
                }
            }
            return salaryDto;
        }
        public async Task<List<CardDto>> GetCards(long id)
        {
            var cards = await _cardRepository.GetAll();
            List<CardDto> cardDtos = new List<CardDto>();
            foreach(var item in cards)
            {
                if(item.EmployeeId == id)
                {
                    var element = _mapper.Map<CardDto>(item);
                    cardDtos.Add(element);
                }
            }
            return cardDtos;
        }
        public async Task<List<AppointmentDto>> GetAppointments(long id)
        {
            var appointments = await _appointmentRepository.GetAll();
            List<AppointmentDto> appointmentDtos = new List<AppointmentDto>();
            foreach (var item in appointments)
            {
                if (item.EmployeeId == id)
                {
                    var element = _mapper.Map<AppointmentDto>(item);
                    appointmentDtos.Add(element);
                }
            }
            return appointmentDtos;
        }
    }
}
