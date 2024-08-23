using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;

namespace AcademicManagmentSystem.API.Contracts
{
    public interface IDeloviRepository : IGenericRepository<Deo>
    {
        Task<IEnumerable<Deo>> GetAllResultsForSubjectAsync(int predmetId);
        Task<IEnumerable<Deo>> GetAllResultsForStudentAsync(int predmetId, int studentId);
        Task<IList<Tip>> GetTipoviForSubject(int predmetId);
    }
}
