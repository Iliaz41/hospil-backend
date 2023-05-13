using Hospital.Dtos.Statistics_dto;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Hospital.Services.IServices;
using System.Drawing.Printing;

namespace Hospital.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHealthStatusRepository _healthStatusRepository;
        public StatisticsService(IEmployeeRepository employeeRepository, IPatientRepository patientRepository, ICardRepository cardRepository, ISalaryRepository salaryRepository, IAppointmentRepository appointmentRepository, IHealthStatusRepository healthStatusRepository)
        {
            _employeeRepository = employeeRepository;
            _patientRepository = patientRepository;
            _cardRepository = cardRepository;
            _salaryRepository = salaryRepository;
            _appointmentRepository = appointmentRepository;
            _healthStatusRepository = healthStatusRepository;
        }

        public async Task<PatientStatisticsDto> GetPatientStatistics(long id)
        {
            Card card = await _cardRepository.GetById(id);
            Patient patient = await _patientRepository.GetById(card.PatientId);
            List<HealthStatus> healthStatuses = await _healthStatusRepository.GetAll();
            List<int> indexes = new List<int>();
            foreach(var item in healthStatuses)
            {
                if(item.PatientId == patient.Id)
                {
                    if((DateTime)card.Date_in < (DateTime)item.Date && (DateTime)card.Date_out > (DateTime)item.Date)
                    {
                        indexes.Add((int)item.Index);
                    }
                }
            }
            float averageIndex = 0;
            int count = 0;
            foreach(var item in indexes)
            {
                averageIndex += item;
                count++;
            }
            averageIndex = averageIndex / count;
            int days = (int)((DateTime)card.Date_out - (DateTime)card.Date_in).TotalDays;
            int amount = 0;
            List<Appointment> appointments = await _appointmentRepository.GetAll();
            foreach(var item in appointments)
            {
                if (item.PatientId == patient.Id)
                {
                    if ((DateTime)card.Date_in < (DateTime)item.DateTime && (DateTime)card.Date_out > (DateTime)item.DateTime)
                    {
                        amount++;
                    }
                }
            }
            PatientStatisticsDto patientStatisticsDto = new PatientStatisticsDto();
            patientStatisticsDto.PatientId = patient.Id;
            patientStatisticsDto.PatientName = patient.Name;
            patientStatisticsDto.PatientSurname = patient.Surname;
            patientStatisticsDto.AverageIndex = averageIndex;
            patientStatisticsDto.Days = days;
            patientStatisticsDto.Appointments = amount;
            return patientStatisticsDto;
        }

        public async Task<EmployeeStatisticsDto> GetEmployeeStatistics(long id)
        {
            Employee employee = await _employeeRepository.GetById(id);
            var cards = await _cardRepository.GetAll();
            List<Card> cardsWithId = new List<Card>();
            foreach(var item in cards)
            {
                if(item.EmployeeId == id)
                {
                    cardsWithId.Add(item);
                }
            }
            var appointments = await _appointmentRepository.GetAll();
            var appointmentsWithId = new List<Appointment>();
            foreach(var item in appointments)
            {
                if(item.EmployeeId == id)
                {
                    appointmentsWithId.Add(item);
                }
            }
            EmployeeStatisticsDto employeeStatisticsDto = new EmployeeStatisticsDto();
            employeeStatisticsDto.EmployeeId = employee.Id;
            employeeStatisticsDto.EmployeeName = employee.Name;
            employeeStatisticsDto.EmployeeSurname = employee.Surname;
            employeeStatisticsDto.AmountHours = (int)employee.Hours;
            employeeStatisticsDto.AmountAppointments = appointmentsWithId.Count();
            employeeStatisticsDto.AmountCards = cardsWithId.Count();
            return employeeStatisticsDto;
        }

        public async Task<HospitalStatisticsDto> GetHospitalStatistics()
        {
            HospitalStatisticsDto hospitalStatisticsDto = new HospitalStatisticsDto();
            var employees = await _employeeRepository.GetAll();
            hospitalStatisticsDto.AmountsEmployees = employees.Count();
            var patients = await _patientRepository.GetAll();
            hospitalStatisticsDto.AmountPatients = patients.Count();
            var cards = await _cardRepository.GetAll();
            hospitalStatisticsDto.AmountsCards = cards.Count();
            var appointments = await _appointmentRepository.GetAll();
            hospitalStatisticsDto.AmountAppointments = appointments.Count();
            var status = await _healthStatusRepository.GetAll();
            float averageIndex = 0;
            foreach(var item in status)
            {
                averageIndex += (float)item.Index;
            }
            averageIndex = averageIndex / status.Count();
            hospitalStatisticsDto.AverageIndex = averageIndex;
            //float averageSalary = 0;
            //foreach(var item in employees)
            //{
            //    Salary salary = await _salaryRepository.GetById(item.Id);
            //    int value = (int)(item.Hours * salary.Kpi);
            //    averageSalary += value;
            //}
            //averageSalary= averageSalary / employees.Count();
            //hospitalStatisticsDto.AverageSalary = averageSalary;
            return hospitalStatisticsDto;
        }
    }
}
