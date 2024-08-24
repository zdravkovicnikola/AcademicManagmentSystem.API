using AcademicManagmentSystem.API.Models;

namespace AcademicManagmentSystem.API.Services.Interface
{
    public interface ICsvService
    {
        public IEnumerable<BasicDTO> LoadCsvData(string filePath);
        
    }
}
