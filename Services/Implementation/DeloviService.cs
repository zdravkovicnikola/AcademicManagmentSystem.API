using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Studenti;
using AcademicManagmentSystem.API.Services.Interface;
using AutoMapper;
using System.Collections.Generic;

namespace AcademicManagmentSystem.API.Services.Implementation
{
    public class DeloviService : IDeloviService
    {
        private readonly IDeloviRepository _deloviRepository;
        private readonly IPredmetiRepository _predmetiRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public DeloviService(IDeloviRepository deloviRepository, IPredmetiRepository predmetiRepository, IStudentRepository studentRepository ,IMapper mapper )
        {
            _deloviRepository = deloviRepository;
            _predmetiRepository = predmetiRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteDeoAsync(int id)
        {
            var deo = await _deloviRepository.GetAsync(id);
            if (deo == null)
            {
                return false; // Deo nije pronađen
            }

            await _deloviRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<StudentDeoDto>> GetAllResultsForSubjectAndStudent(string sifraPredmeta, string indeks)
        {
            var predmet = await _predmetiRepository.GetAsyncByPass(sifraPredmeta);
            if (predmet == null)
            {
                throw new InvalidOperationException("Ne postoji predmet sa zadatom šifrom. Molimo proverite unos.");
            }

            var student = await _studentRepository.GetAsyncByIndex(indeks);
            if (student == null)
            {
                throw new InvalidOperationException($"Ne postoji student sa zadatim indeksom. Molimo proverite unos u formatu 'XXXX/YYYY'.");
            }

            var delovi = await _deloviRepository.GetAllResultsForStudentAsync(predmet.PredmetId, student.StudentId);

            var deloviDto = _mapper.Map<IEnumerable<StudentDeoDto>>(delovi);
            return deloviDto;
        }

        public async Task<IEnumerable<DeoDto>> GetAllResultsForSubject(string sifraPredmeta)
        {
            var predmet = await _predmetiRepository.GetAsyncByPass(sifraPredmeta);
            if (predmet == null)
            {
                throw new InvalidOperationException("Ne postoji predmet sa zadatom šifrom. Molimo proverite unos.");
            }

            var delovi = await _deloviRepository.GetAllResultsForSubjectAsync(predmet.PredmetId);

            var deloviDto = _mapper.Map<IEnumerable<DeoDto>>( delovi);
            return deloviDto;
        }

        public async Task<IList<Tip>> GetTipoviForSubject(int predmetId)
        {
            var tipovi = await _deloviRepository.GetTipoviForSubject(predmetId);
            if (tipovi == null)
            {
                throw new InvalidOperationException("Ne postoje definisani delovi za predmet sa zadatom šifrom. ");
            }
            return tipovi;
        }

        public async Task<IEnumerable<StudentWithDeoDto>> GetResultsByPredmet(string sifraPredmeta, DateTime startDate, DateTime endDate)
        {
            var predmet = await _predmetiRepository.GetAsyncByPass(sifraPredmeta);
            if (predmet == null)
            {
                throw new InvalidOperationException("Ne postoji predmet sa zadatom šifrom. Molimo proverite unos.");
            }

            var students = await _studentRepository.GetStudentsByPredmetId(predmet.PredmetId, startDate, endDate);
            var result = students.Select(student =>
            {
                var studentDto = _mapper.Map<StudentWithDeoDto>(student);
                studentDto.Delovi = student.Delovi
                    .Where(d => d.PredmetId == predmet.PredmetId)
                    .Select(d => _mapper.Map<DeoForStudentDto>(d))
                    .ToList();
                return studentDto;
            }).ToList();

            return result;
        }
    }
}
