using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Contracts
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        Task<Student> GetAsyncByIndex(string indeks);
        Task<Student> GetAsyncStudent (string? index);
        Task<IEnumerable<Student>> GetStudentsByPredmetId(int predmetId, DateTime startDate, DateTime endDate);
    }
}
