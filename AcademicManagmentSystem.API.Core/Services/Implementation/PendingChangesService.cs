﻿using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models;
using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Studenti;
using AcademicManagmentSystem.API.Core.Services.Interface;
using AutoMapper;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class PendingChangesService : IPendingChangesService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;
        private readonly IDeloviRepository _deloviRepository;
        private readonly IPendingChangesStore _pendingStudentsDictionary;
        private readonly IMapper _mapper;
        public List<Student> pendingStudents = new List<Student>();
        public List<Student> rollbackStudents = new List<Student>();

        public PendingChangesService(
            IStudentRepository studentRepository,
            IMapper mapper,
            IEmailService emailService,
            IDeloviRepository deloviRepository,
            IPendingChangesStore pendingChangesStore)
        {
            _pendingStudentsDictionary = pendingChangesStore;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _emailService = emailService;
            _deloviRepository = deloviRepository;
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
        public async Task ProcessPendingUsmeni(BasicDTO record)
        {
            var ime = record.Data[0];
            var prezime = record.Data[1];
            var index = _emailService.ExtractIndexFromEmail(record.Data[2]);
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

        public async Task<List<PendingStudentDto>> ReturnListPendingStudents()
        {
            //GUID
            var guid = Guid.NewGuid();

            //mapiranje Studenta u PendingStudentDto
            var result = pendingStudents.Select(student =>
            {
                var studentDto = _mapper.Map<PendingStudentDto>(student);
                var deo = student.Delovi.FirstOrDefault();
                studentDto.Deo = deo != null ? _mapper.Map<DeoForPendingStudentDto>(deo) : null;
                return studentDto;
            }).ToList();

            // Cuvaj listu u dictionary sa GUID-om kao ključem
            _pendingStudentsDictionary.AddPendingStudents(guid, result);
            pendingStudents = new List<Student>();
            // Vracamo GUID i listu kao tuple
            return (result);
        }

        public async Task<bool> CommitPendingChanges(Guid guid)
        {
            // Preuzmi pending studente na osnovu GUID-a
            var pendingStudents = _pendingStudentsDictionary.GetPendingStudents(guid);

            if (pendingStudents == null || !pendingStudents.Any())
            {
                return false;
            }

            // Pripremi rollback data
            var rollbackData = new List<PendingStudentDto>();

            try
            {
                foreach (var pendingStudent in pendingStudents)
                {
                    // Učitaj studenta iz baze zajedno sa svim delovima
                    var studentFromDb = await _studentRepository.GetAsyncStudent(pendingStudent.Index);

                    
                    if (studentFromDb == null)
                    {
                        continue;
                    }

                    var deoToUpdate = studentFromDb.Delovi.FirstOrDefault(d => d.DeoId == pendingStudent.Deo.DeoId);
                    
                    if (deoToUpdate != null && pendingStudent.Deo.BrojPoena > deoToUpdate.BrojPoena)
                    {
                        //Dodajemo u rollback pre update-a
                        var studentDto = _mapper.Map<PendingStudentDto>(studentFromDb);
                        studentDto.Deo = deoToUpdate != null ? _mapper.Map<DeoForPendingStudentDto>(deoToUpdate) : null;
                        rollbackData.Add(studentDto);

                        // Update
                        deoToUpdate.BrojPoena = pendingStudent.Deo.BrojPoena;
                        deoToUpdate.Napomena = pendingStudent.Deo.Napomena;
                        deoToUpdate.Polozio = pendingStudent.Deo.Polozio;

                        // Save
                        await _deloviRepository.UpdateAsync(deoToUpdate);
                    }
                }

                // Sačuvaj rollback data
                _pendingStudentsDictionary.AddToRollbackStore(guid, rollbackData);

                // Ukloni pending promene nakon uspešnog commita
                _pendingStudentsDictionary.RemovePendingStudents(guid);

                return true;
            }
            catch (Exception)
            {
                // U slučaju greške, vrati prethodno stanje
                await RollbackChanges(guid);
                return false;
            }
        }
        public async Task<bool> RollbackChanges(Guid guid)
        {
            var rollbackData = _pendingStudentsDictionary.GetRollbackData(guid);

            if (rollbackData == null || !rollbackData.Any())
            {
                return false;
            }
            foreach(var rollbackStudent in  rollbackData) { 

                var studentFromDb = await _studentRepository.GetAsyncStudent(rollbackStudent.Index);


                if (studentFromDb == null)
                {
                    continue;
                }

                var deoToUpdate = studentFromDb.Delovi.FirstOrDefault(d => d.DeoId == rollbackStudent.Deo.DeoId);

                if (deoToUpdate != null)
                {

                    // Update
                    deoToUpdate.BrojPoena = rollbackStudent.Deo.BrojPoena;
                    deoToUpdate.Napomena = rollbackStudent.Deo.Napomena;
                    deoToUpdate.Polozio = rollbackStudent.Deo.Polozio;

                    // Save
                    await _deloviRepository.UpdateAsync(deoToUpdate);
                    }
            }

            // Ukloni rollback podatke nakon izvršenog rollback-a
            _pendingStudentsDictionary.RemoveRollbackData(guid);
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

        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllPendingChanges()
        {
            return _pendingStudentsDictionary.GetAllPendingChanges();
        }

        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllRollbacks()
        {
            return _pendingStudentsDictionary.GetRollbackData();
        }

        public async Task<bool> RemovePendingChanges(Guid guid)
        {
            // Preuzmi pending studente na osnovu GUID-a
            var pendingStudents = _pendingStudentsDictionary.GetPendingStudents(guid);

            if (pendingStudents == null || !pendingStudents.Any())
            {
                return false;
            }

            // Ukloni pending studente iz dictionary-ja
            _pendingStudentsDictionary.RemovePendingStudents(guid);

            return true;
        }


    }
}