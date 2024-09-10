using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Contracts
{
    public interface IPredmetiRepository : IGenericRepository<Predmet>
    {
        Task<Predmet> GetDetailsPredavac(int id);
        Task<Predmet> GetAsyncByPass (string sifra);
        Task<int> GetBrojTipovaDelova(int predmetId);

    }
}
