using Hospital.Models;
using Hospital.Repository.IRepository;
using Hospital.Services.IServices;
using System.Drawing;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Hospital.Services
{
    public class DocxService : IDocxService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICardRepository _cardRepository;
        public DocxService(IPatientRepository patientRepository, IEmployeeRepository employeeRepository, IAppointmentRepository appointmentRepository, ICardRepository cardRepository)
        {
            _patientRepository = patientRepository;
            _employeeRepository = employeeRepository;
            _appointmentRepository = appointmentRepository;
            _cardRepository = cardRepository;
        }
        public async Task CreateDocument(long id)
        {
            var card = await _cardRepository.GetById(id);
            string path = "E:/record/";
            Patient patient = await _patientRepository.GetById(card.PatientId);
            string filepath = String.Concat(path, patient.Surname, "#", card.Id);
            DocX document = DocX.Create(filepath);
            Paragraph paragraph1 = document.InsertParagraph();
            paragraph1.Append($"Card number: {card.Id.ToString()}").FontSize(14).Font("TimesNewRoman").Italic();
            paragraph1.Alignment = Alignment.center;
            Paragraph paragraph2 = document.InsertParagraph();
            paragraph2.Append($"\n\nPerson: {patient.Surname} {patient.Name}").FontSize(14).Font("TimesNewRoman").Italic();
            paragraph2.Alignment = Alignment.left;
            Paragraph paragraph3 = document.InsertParagraph();
            paragraph3.Append($"Date of birthday: {patient.DateOfBirth.ToString()}").FontSize(14).Font("TimesNewRoman").Italic();
            Paragraph paragraph4 = document.InsertParagraph();
            paragraph4.Append($"Address: {patient.City} {patient.Street} {patient.House}").FontSize(14).Font("TimesNewRoman").Italic();
            Paragraph paragraph5 = document.InsertParagraph();
            paragraph5.Append($"Date in: {card.Date_in.ToString()}, date out: {card.Date_out.ToString()}").FontSize(14).Font("TimesNewRoman").Italic();
            Paragraph paragraph6 = document.InsertParagraph();
            paragraph6.Append($"Diagnosys: {card.Diagnosys}").FontSize(14).Font("TimesNewRoman").Italic();
            List<Appointment> appointments = await _appointmentRepository.GetAll();
            List<Appointment> needed = new List<Appointment>();
            foreach(var item in appointments)
            {
                if(item.PatientId == patient.Id)
                {
                    needed.Add(item);
                }
            }
            DateTime date_in = (DateTime)card.Date_in;
            DateTime date_out = (DateTime)card.Date_out;
            foreach(Appointment appointment in needed)
            {
                var date = appointment.DateTime;
                if(date_in < date && date_out > date)
                {
                    Paragraph paragraph = document.InsertParagraph();
                    paragraph.Append($"{appointment.DateTime.ToString()}, Procedure: {appointment.Title}, Result: {appointment.Result}").FontSize(14).Font("TimesNewRoman").Italic();
                }
            }
            var employee = await _employeeRepository.GetById(card.EmployeeId);
            Paragraph paragraph8 = document.InsertParagraph();
            paragraph8.Append($"\n\n\n\n\n\n{DateTime.Today.ToString()}                                               {employee.Name} {employee.Surname}").FontSize(14).Font("TimesNewRoman").Italic();
            document.Save();
        }
    }
}
