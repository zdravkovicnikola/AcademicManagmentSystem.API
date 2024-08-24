using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Studenti;
using AcademicManagmentSystem.API.Repository;
using AcademicManagmentSystem.API.Services.Interface;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicManagmentSystem.API.Services.Implementation
{
    public class PendingChangesService : IPendingChangesService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public static List<Student> pendingStudents = new List<Student>();

        public PendingChangesService(
            IServiceScopeFactory serviceScopeFactory, 
            IMapper mapper)
        { 
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public async Task ProcessPendingPrakticni(BasicDTO record)
        {
            var ime = record.Data[0];
            var prezime = record.Data[1];
            while (record.Data[3].Length < 4)
            {
                record.Data[3] = "0" + record.Data[3];
            }
            var index = record.Data[3] + "/" + record.Data[2];
            var eliminacioni = record.Data[4] == "1";
            var poeniString = record.Data[5].Replace(",", ".");

            if (!double.TryParse(poeniString, out double poeni))
            {
                return; // Ako poeni nisu validni, ne vracamo nista
            }

            DateTime dateTime = DateTime.Now;
            string formattedDatum = dateTime.ToString("d.M.yyyy");

            if (formattedDatum == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();

                var student = await _studentRepository.GetAsyncStudent(index);
                if (student == null)
                {
                    return;
                }

                var prakticni = student.Delovi
                    .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 2);

                if (prakticni == null)
                {
                    return;
                }
                if (prakticni.BrojPoena != poeni)
                {
                    student.Delovi = new List<Deo>();//student.Delovi.Remove(prakticni);  
                    prakticni.Napomena = $"Promena poena na uvidu: Stari = {prakticni.BrojPoena}, Novi = {poeni} ";
                    prakticni.BrojPoena = poeni;
                    if (eliminacioni && !prakticni.Polozio && prakticni.BrojPoena > 51)
                    {
                        prakticni.Napomena += $"Promena statusa: Položio";
                        prakticni.Polozio = true;
                    }
                    student.Delovi.Add(prakticni);
                    pendingStudents.Add(student);
                }
            }
        }
        public async Task ProcessPendingUsmeni(BasicDTO record)
        {
            var ime = record.Data[0];
            var prezime = record.Data[1];
            var index = "";
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            
                index = _emailService.ExtractIndexFromEmail(record.Data[2]);
            }
            var status = record.Data[3];
            var datum = ReplaceSerbianMonths(record.Data[4]);
            var poeniString = record.Data[7].Replace(",", ".");

            if (!double.TryParse(poeniString, out double poeni))
            {
                return;
            }

            if (!DateTime.TryParse(datum, out DateTime dateTime))
            {
                return;
            }

            var formattedDatum = dateTime.ToString("d.M.yyyy");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();

                var student = await _studentRepository.GetAsyncStudent(index);
                if (student == null)
                {
                    return;
                }

                var usmeni = student.Delovi
                .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 1);

                if (usmeni == null)
                {
                    return;
                }
                if (usmeni.BrojPoena != poeni)
                {
                    student.Delovi = new List<Deo>();//student.Delovi.Remove(prakticni);  
                    usmeni.Napomena = $"Promena poena na uvidu: Stari = {usmeni.BrojPoena}, Novi = {poeni} ";
                    usmeni.BrojPoena = poeni;
                    if (status == "Завршени" && !usmeni.Polozio && usmeni.BrojPoena > 51)
                    {
                        usmeni.Napomena += $"Promena statusa: Položio";
                        usmeni.Polozio = true;
                    }
                    student.Delovi.Add(usmeni);
                    pendingStudents.Add(student);
                }
            }
        }

        public async Task<List<PendingStudentDto>> ReturnListPendingStudents()
        {
            var result = pendingStudents.Select(student =>
            {
                var studentDto = _mapper.Map<PendingStudentDto>(student);
                var deo = student.Delovi.FirstOrDefault();
                studentDto.Deo = _mapper.Map<DeoForPendingStudentDto>(deo);          
                return studentDto;
            }
            ).ToList();

            return result;
        }
        public async Task<bool> CommitPendingChanges()
        {
            if (pendingStudents.Count == 0)
            {
                return false;
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
                var _deloviRepository = scope.ServiceProvider.GetRequiredService<IDeloviRepository>();

                foreach (var pendingStudent in pendingStudents)
                {
                    // Učitaj studenta iz baze zajedno sa svim delovima
                    var studentFromDb = await _studentRepository.GetAsyncStudent(pendingStudent.Index);

                    if (studentFromDb == null)
                    {
                        continue;
                    }

                    foreach (var pendingDeo in pendingStudent.Delovi)
                    {
                        var deoToUpdate = studentFromDb.Delovi.FirstOrDefault(d => d.DeoId == pendingDeo.DeoId);
                        if (deoToUpdate != null)
                        {
                            // Update relevant fields
                            deoToUpdate.BrojPoena = pendingDeo.BrojPoena;
                            deoToUpdate.Napomena = pendingDeo.Napomena;
                            deoToUpdate.Polozio = pendingDeo.Polozio;

                            // Save changes
                            await _deloviRepository.UpdateAsync(deoToUpdate);
                        }
                    }

                    await _studentRepository.UpdateAsync(studentFromDb);
                }
            }

            pendingStudents.Clear(); // Brisanje liste nakon uspešnog commita
            return true;
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
