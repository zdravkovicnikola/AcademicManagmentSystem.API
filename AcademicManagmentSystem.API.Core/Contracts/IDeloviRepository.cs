using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Delovi;

namespace AcademicManagmentSystem.API.Contracts
{
    public interface IDeloviRepository : IGenericRepository<Deo>
    {
        Task<IEnumerable<Deo>> GetAllResultsForSubjectAsync(int predmetId, DateTime datum, int tipId);
        Task<IEnumerable<Deo>> GetAllResultsForStudentAsync(int predmetId, int studentId);
        Task<IList<Tip>> GetTipoviForSubject(int predmetId);
    }
}
