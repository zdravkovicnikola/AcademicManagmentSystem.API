using AcademicManagmentSystem.API.Contracts;
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
        private readonly IOcenaService _ocenaService;
        private readonly IMapper _mapper;
        public List<Student> pendingStudents = new List<Student>();

        public PendingChangesService(
            IStudentRepository studentRepository,
            IMapper mapper,
            IEmailService emailService,
            IDeloviRepository deloviRepository,
            IPendingChangesStore pendingChangesStore,
            IOcenaService ocenaService)
        {
            _pendingStudentsDictionary = pendingChangesStore;
            _ocenaService = ocenaService;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _emailService = emailService;
            _deloviRepository = deloviRepository;
        }

        public async Task ProcessPendingPrakticni(BasicDTO record, int predmetId)
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
                .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 2 && d.PredmetId == predmetId);

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
                    prakticni.Napomena += $"Promena statusa: Položen";
                    prakticni.Polozio = true;
                }
                student.Delovi.Add(prakticni);
                pendingStudents.Add(student);
            }

        }
        public async Task ProcessPendingUsmeni(BasicDTO record, int predmetid)
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
            .FirstOrDefault(d => d.Datum.Date == DateTime.Parse(formattedDatum) && d.TipId == 1 && d.PredmetId == predmetid);

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
                    usmeni.Napomena += $"Promena statusa: Položen";
                    usmeni.Polozio = true;
                }
                student.Delovi.Add(usmeni);
                pendingStudents.Add(student);
            }

        }
        public async Task<List<PendingStudentDto>> ReturnListPendingStudents()
        {
            var guid = Guid.NewGuid();

            var result = pendingStudents.Select(student =>
            {
                var studentDto = _mapper.Map<PendingStudentDto>(student);
                var deo = student.Delovi.FirstOrDefault();
                studentDto.Deo = deo != null ? _mapper.Map<DeoForPendingStudentDto>(deo) : null;
                return studentDto;
            }).ToList();

            _pendingStudentsDictionary.AddPendingStudents(guid, result);
            pendingStudents = new List<Student>();

            return (result);
        }
        public async Task<bool> CommitPendingChanges(Guid guid)
        {
            
            var pendingStudents = _pendingStudentsDictionary.GetPendingStudents(guid);

            if (pendingStudents.Count == 0 || !pendingStudents.Any())
            {
                return false;
            }

            var rollbackData = new List<PendingStudentDto>();

            try
            {
                foreach (var pendingStudent in pendingStudents)
                {
                    var studentFromDb = await _studentRepository.GetAsyncStudent(pendingStudent.Index);

                    if (studentFromDb == null)
                    {
                        continue;
                    }

                    var deoToUpdate = studentFromDb.Delovi.FirstOrDefault(d => d.DeoId == pendingStudent.Deo.DeoId);

                    if (deoToUpdate != null && pendingStudent.Deo.BrojPoena > deoToUpdate.BrojPoena)
                    {

                        var studentDto = _mapper.Map<PendingStudentDto>(studentFromDb);
                        studentDto.Deo = deoToUpdate != null ? _mapper.Map<DeoForPendingStudentDto>(deoToUpdate) : null;
                        rollbackData.Add(studentDto);

                        studentFromDb.Delovi.Remove(deoToUpdate);

                        deoToUpdate.BrojPoena = pendingStudent.Deo.BrojPoena;
                        deoToUpdate.Napomena = pendingStudent.Deo.Napomena;
                        deoToUpdate.Polozio = pendingStudent.Deo.Polozio;

                        if (deoToUpdate.Polozio)
                        {
                            studentFromDb.Delovi.Add(deoToUpdate);
                            var ocena = await _ocenaService.IzracunajOcenuZaStudenta(studentFromDb, deoToUpdate.PredmetId);
                            
                            studentFromDb.Ocene.Add(new Ocena
                            {
                                StudentId = studentFromDb.StudentId,
                                PredmetId = deoToUpdate.PredmetId,
                                DatumPolaganja = DateTime.Now,
                                VrednostOcene = ocena
                            });
                            if(ocena != 5)
                            deoToUpdate.Napomena += $" Kompletiran ispit  ocenom {ocena}.";
                            await _studentRepository.UpdateAsync(studentFromDb);
                        }

                        await _deloviRepository.UpdateAsync(deoToUpdate);
                    }
                }

                _pendingStudentsDictionary.AddToRollbackStore(guid, rollbackData);

                _pendingStudentsDictionary.RemovePendingStudents(guid);

                return true;
            }
            catch (Exception)
            {
                await RollbackChanges(guid);
                return false;
            }
        }

        public async Task<bool> CancelCommit(Guid guid)
        {
            var commitData = _pendingStudentsDictionary.GetCommitData(guid);

            if (commitData == null || !commitData.Any())
            {
                return false;
            }
            foreach(var rollbackStudent in commitData) { 

                var studentFromDb = await _studentRepository.GetAsyncStudent(rollbackStudent.Index);


                if (studentFromDb == null)
                {
                    continue;
                }

                var deoToUpdate = studentFromDb.Delovi.FirstOrDefault(d => d.DeoId == rollbackStudent.Deo.DeoId);

                if (deoToUpdate != null)
                {
                    studentFromDb.Ocene.Remove(studentFromDb.Ocene.LastOrDefault());
                    // Update
                    deoToUpdate.BrojPoena = rollbackStudent.Deo.BrojPoena;
                    deoToUpdate.Napomena = rollbackStudent.Deo.Napomena;
                    deoToUpdate.Polozio = rollbackStudent.Deo.Polozio;

                    // Save
                    await _studentRepository.UpdateAsync(studentFromDb);
                    await _deloviRepository.UpdateAsync(deoToUpdate);
                }
            }

            // Ukloni rollback podatke nakon izvršenog rollback-a
            _pendingStudentsDictionary.RemoveCommitData(guid);
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
            return _pendingStudentsDictionary.GetCommitData();
        }
        public async Task<bool> RollbackChanges(Guid guid)
        {
            
            var pendingStudents = _pendingStudentsDictionary.GetPendingStudents(guid);

            //if (pendingStudents == null || !pendingStudents.Any())
            //{
            //    return false;
            //}

            _pendingStudentsDictionary.RemovePendingStudents(guid);

            return true;
        }


    }
}