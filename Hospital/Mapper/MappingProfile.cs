using AutoMapper;
using Hospital.Dtos.Card_dto;
using Hospital.Dtos.Employee_dto;
using Hospital.Dtos.Patient_dto;
using Hospital.Models;
using Hospital.Dtos.Appointment_dto;
using Hospital.Dtos.HealthStatus_dto;
using Hospital.Dtos.Salary_dto;
using Hospital.Dtos.Photo_dto;

namespace Hospital.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<Card, CreateCardDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Patient, CreatePatientDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<HealthStatus, HealthStatusDto>().ReverseMap();
            CreateMap<HealthStatus, CreateHealthStatusDto>().ReverseMap();
            CreateMap<Salary, SalaryDto>().ReverseMap();
            CreateMap<Salary, CreateSalaryDto>().ReverseMap();
            CreateMap<IFormPhotoDto, StrPhotoDto>().ReverseMap();
        }
    }
}
