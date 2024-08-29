using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models;
using AcademicManagmentSystem.API.Core.Services.Interface;
using Microsoft.Extensions.Logging;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class UploadExamService : IUploadExamService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDeloviRepository _deloviRepository;
        private readonly ITipRepository _tipRepository;
        private readonly IPredmetiRepository _predmetiRepository;
        private readonly ILogger<UploadExamService> _logger;
        private readonly IEmailService _emailService;

        public UploadExamService(
            IStudentRepository studentRepository,
            IDeloviRepository deloviRepository,
            ITipRepository tipRepository,
            IPredmetiRepository predmetiRepository,
            ILogger<UploadExamService> logger,
            IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _deloviRepository = deloviRepository;
            _tipRepository = tipRepository;
            _predmetiRepository = predmetiRepository;
            _logger = logger;
            _emailService = emailService;
        }
        public async Task ProcessOrUpdatePrakticniRecord(BasicDTO record, bool isUpdate)
        {
            var ime = record.Data[0];
            var prezime = record.Data[1];
            while (record.Data[3].Length < 4)
            {
                record.Data[3] = "0" + record.Data[3];
            }
            var index = record.Data[3] + "/" + record.Data[2];
            var eliminacioni = record.Data[4] == "1"; // Primer: Eliminacioni deo
            var poeniString = record.Data[5].Replace(",", ".");

            if (!double.TryParse(poeniString, out double poeni))
            {
                _logger.LogWarning($"Invalid poeni format: {poeniString}");
                return;
            }
            _logger.LogInformation($"Processing student {index}");

            DateTime dateTime = DateTime.Now;
            string formattedDatum = dateTime.ToString("d.M.yyyy");

            if (formattedDatum == null)
            {
                _logger.LogWarning($"Invalid date format in row: {formattedDatum}");
                return; 
            }

            var student = await _studentRepository.GetAsyncStudent(index);
            if (student == null)
            {
                student = new Student
                {
                    Ime = ime,
                    Prezime = prezime,
                    Index = index,
                    Ocene = new List<Ocena>(),
                    Delovi = new List<Deo>()
                };
                await _studentRepository.AddAsync(student);
                _logger.LogInformation($"Student added");
            }
            var prakticni = student.Delovi
                .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 2);

            if (prakticni == null) { 
            prakticni = new Deo
            {
                TipId = 2,
                Tip = await _tipRepository.GetAsync(2),
                StudentId = student.StudentId,
                Student = student,
                BrojPoena = poeni,
                Datum = DateTime.Parse(formattedDatum),
                PredmetId = 1,
                Predmet = await _predmetiRepository.GetAsync(1),
                Napomena = "Nema napomene."
            };

            student.Delovi.Add(prakticni); // Dodajemo prakticni deo studentu
            await _deloviRepository.AddAsync(prakticni);
            _logger.LogInformation($"Prakticni added");
            } ////////////
            else if (isUpdate && prakticni.BrojPoena < poeni)
            {
                prakticni.BrojPoena = poeni;
                await _deloviRepository.UpdateAsync(prakticni);
                _logger.LogInformation($"Deo updated");
            }
            if (eliminacioni && prakticni.BrojPoena > 51)
            {
                prakticni.Polozio = true;

                await _deloviRepository.UpdateAsync(prakticni); // Azuriraj entitet Deo
                _logger.LogInformation($"Deo updated");

            }
            await _studentRepository.UpdateAsync(student); // Azuriraj entitet Student
            _logger.LogInformation($"Student updated");
        }

        public async Task ProcessOrUpdateUsmeniRecord(BasicDTO record, bool isUpdate)
        {
            var ime = record.Data[0];
            var prezime = record.Data[1]; 
            var index = _emailService.ExtractIndexFromEmail(record.Data[2]); 
            var status = record.Data[3];
            var datum = ReplaceSerbianMonths(record.Data[4]); 
            var poeniString = record.Data[7].Replace(",", ".");

            if (!double.TryParse(poeniString, out double poeni))
            {
                _logger.LogWarning($"Invalid poeni format: {poeniString}");
                return;
            }

            if (!DateTime.TryParse(datum, out DateTime dateTime))
            {
                _logger.LogWarning($"Invalid date format in row: {datum}");
                return;
            }

            var formattedDatum = dateTime.ToString("d.M.yyyy");
            _logger.LogInformation($"Processing student {index}");

            var student = await _studentRepository.GetAsyncStudent(index);
            if (student == null)
            {
                student = new Student
                {
                    Ime = ime,
                    Prezime = prezime,
                    Index = index,
                    Ocene = new List<Ocena>(),
                    Delovi = new List<Deo>()
                };
                await _studentRepository.AddAsync(student);
                _logger.LogInformation($"Student added");
            }

            var usmeni = student.Delovi
                .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 1);

            if (usmeni == null)
            {
                usmeni = new Deo
                {
                    TipId = 1,
                    Tip = await _tipRepository.GetAsync(1),
                    StudentId = student.StudentId,
                    Student = student,
                    BrojPoena = poeni,
                    Datum = dateTime,
                    PredmetId = 1,
                    Predmet = await _predmetiRepository.GetAsync(1),
                    Napomena = "Nema napomene."
                };
                student.Delovi.Add(usmeni);
                await _deloviRepository.AddAsync(usmeni);
                _logger.LogInformation($"Usmeni added");
            }
            else if (isUpdate && usmeni.BrojPoena < poeni)
            {
                usmeni.BrojPoena = poeni;
                await _deloviRepository.UpdateAsync(usmeni);
                _logger.LogInformation($"Deo updated");
            }

            if (status == "Завршени" && usmeni.BrojPoena > 51)
            {
                usmeni.Polozio = true;
                await _deloviRepository.UpdateAsync(usmeni);
                _logger.LogInformation($"Deo updated");
            }

            await _studentRepository.UpdateAsync(student);
            _logger.LogInformation($"Student updated");
        }

        private string ReplaceSerbianMonths(string datum)
        {
            return datum
                .Replace("јул", "7.")
                .Replace("август", "8.")
                .Replace("септембар", "9.")
                .Replace("октобар", "10.")
                .Replace("новембар", "11.")
                .Replace("децембар", "12.")
                .Replace("јануар", "1.")
                .Replace("фебруар", "2.")
                .Replace("март", "3.")
                .Replace("април", "4.")
                .Replace("мај", "5.")
                .Replace("јун", "6.");
        }
    }
}
