using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Contracts
{
    public interface IPredmetiRepository : IGenericRepository<Predmet>
    {
        Task<Predmet> GetDetailsPredavac(int id);
        Task<Predmet> GetDetailsDeo(int id);
    }
}
