using AutoMapper;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.HealthStatus_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Hospital.Services.IServices;

namespace Hospital.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHealthStatusRepository _healthStatusRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IEmployeeRepository employeeRepository, ICardRepository cardRepository, IAppointmentRepository appointmentRepository, IHealthStatusRepository healthStatusRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _employeeRepository = employeeRepository;
            _cardRepository = cardRepository;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _healthStatusRepository = healthStatusRepository;
        }

        public async Task<List<AppointmentDto>> GetAppointments(long id)
        {
            var appointments = await _appointmentRepository.GetAll();
            var appointmentDtos = new List<AppointmentDto>();
            foreach(var item in appointments)
            {
                if(item.PatientId == id)
                {
                    var element = _mapper.Map<AppointmentDto>(item);
                    appointmentDtos.Add(element);
                }
            }
            return appointmentDtos;
        }

        public async Task<List<PatientDto>> GetByBirthday(DateTime date)
        {
            var patients = await _patientRepository.GetAll();
            var patientDtos = new List<PatientDto>();
            foreach(var item in patients)
            {
                if(item.DateOfBirth == date)
                {
                    var element = _mapper.Map<PatientDto>(item);
                    patientDtos.Add(element);
                }
            }
            return patientDtos;
        }

        public async Task<List<PatientDto>> GetByName(string name)
        {
            var patients = await _patientRepository.GetAll();
            var patientDtos = new List<PatientDto>();
            foreach (var item in patients)
            {
                if (item.Name == name)
                {
                    var element = _mapper.Map<PatientDto>(item);
                    patientDtos.Add(element);
                }
            }
            return patientDtos;
        }

        public async Task<List<PatientDto>> GetByPassport(string passport)
        {
            var patients = await _patientRepository.GetAll();
            var patientDtos = new List<PatientDto>();
            foreach (var item in patients)
            {
                if (item.Passport == passport)
                {
                    var element = _mapper.Map<PatientDto>(item);
                    patientDtos.Add(element);
                }
            }
            return patientDtos;
        }

        public async Task<List<PatientDto>> GetByRoom(int room)
        {
            var patients = await _patientRepository.GetAll();
            var patientDtos = new List<PatientDto>();
            foreach (var item in patients)
            {
                if (item.Room == room)
                {
                    var element = _mapper.Map<PatientDto>(item);
                    patientDtos.Add(element);
                }
            }
            return patientDtos;
        }

        public async Task<List<PatientDto>> GetBySurname(string surname)
        {
            var patients = await _patientRepository.GetAll();
            var patientDtos = new List<PatientDto>();
            foreach (var item in patients)
            {
                if (item.Surname == surname)
                {
                    var element = _mapper.Map<PatientDto>(item);
                    patientDtos.Add(element);
                }
            }
            return patientDtos;
        }

        public async Task<List<CardDto>> GetCards(long id)
        {
            var cards = await _cardRepository.GetAll();
            var cardDtos = new List<CardDto>();
            foreach(var item in cards)
            {
                if(item.PatientId == id)
                {
                    var element = _mapper.Map<CardDto>(item);
                    cardDtos.Add(element);
                }
            }
            return cardDtos;
        }

        public async Task<EmployeeDto> GetEmployee(long id)
        {
            var cards = await _cardRepository.GetAll();
            List<Card> cardsWithId = new List<Card>();
            foreach(var item in cards)
            {
                if(item.PatientId == id)
                {
                    cardsWithId.Add(item);
                }
            }
            long index = 0;
            foreach(var elem in cardsWithId)
            {
                if(elem.Date_out == null)
                {
                    index = elem.EmployeeId;
                }
            }
            var employee = await _employeeRepository.GetById(index);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees(long id)
        {
            var cards = await _cardRepository.GetAll();
            List<Card> cardsWithId = new List<Card>();
            foreach (var item in cards)
            {
                if (item.PatientId == id)
                {
                    cardsWithId.Add(item);
                }
            }
            List<EmployeeDto> employees = new List<EmployeeDto>();
            foreach(var element in cardsWithId)
            {
                var i = await _employeeRepository.GetById(element.EmployeeId);
                employees.Add(_mapper.Map<EmployeeDto>(i));
            }
            return employees;
        }

        public async Task<List<HealthStatusDto>> GetHealthStatuses(long id)
        {
            var statuses = await _healthStatusRepository.GetAll();
            var statusDtos = new List<HealthStatusDto>();
            foreach(var item in statuses)
            {
                if(item.PatientId == id)
                {
                    var element = _mapper.Map<HealthStatusDto>(item);
                    statusDtos.Add(element);
                }
            }
            return statusDtos;
        }
    }
}
