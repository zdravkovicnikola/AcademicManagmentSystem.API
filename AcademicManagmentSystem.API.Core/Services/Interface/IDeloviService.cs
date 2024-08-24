using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Studenti;

namespace AcademicManagmentSystem.API.Services.Interface
{
    public interface IDeloviService
    {
        
        Task<IEnumerable<StudentDeoDto>> GetAllResultsForSubjectAndStudent(string sifraPredmeta, string indeks);
        Task<IEnumerable<DeoDto>> GetAllResultsForSubject(string sifraPredmeta);
        Task<IList<Tip>> GetTipoviForSubject(int id);
        Task<bool> DeleteDeoAsync(int id);
        Task<IEnumerable<StudentWithDeoDto>> GetResultsByPredmet(string sifraPredmeta, DateTime startDate, DateTime endDate);
    }
}
