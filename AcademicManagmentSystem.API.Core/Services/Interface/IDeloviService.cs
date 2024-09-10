using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Studenti;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface IDeloviService
    {
        
        Task<IEnumerable<StudentDeoDto>> GetAllResultsForSubjectAndStudent(string sifraPredmeta, string indeks);
        Task<IEnumerable<DeoDto>> GetAllResultsForSubject(int predmetId, DateTime datum, int tipId);
        Task<IList<Tip>> GetTipoviForSubject(int id);
        Task<bool> DeleteDeoAsync(int id);
        Task<IEnumerable<StudentWithDeoDto>> GetResultsByPredmet(string sifraPredmeta, DateTime startDate, DateTime endDate);
    }
}
