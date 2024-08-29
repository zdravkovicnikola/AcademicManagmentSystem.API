using AcademicManagmentSystem.API.Core.Models;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface ICsvService
    {
        public IEnumerable<BasicDTO> LoadCsvData(string filePath);
        
    }
}
