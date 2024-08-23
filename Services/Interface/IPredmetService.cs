using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Predmeti;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace AcademicManagmentSystem.API.Services.Interface
{
    public interface IPredmetService
    {
        Task<int?> GetPredmetIdBySifraAsync(string sifra);
        Task <IEnumerable<GetPredmetDto>> GetAllSubjects();
        Task <GetPredmetDetailsDto> GetDetailsForSubject(int id);

        Task<Predmet> CreateSubject (CreatePredmetDto createPredmetDto);
        Task<Predmet> UpdateSubject (int id, UpdatePredmetDto updatePredmetDto);

        Task<bool> DeletePredmetAsync(int id);
    }
}
