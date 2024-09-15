using AcademicManagmentSystem.API.Core.Models;
using Microsoft.AspNetCore.Http;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface ICsvService
    {
        public IEnumerable<BasicDTO> LoadCsvData(string filePath);
        public bool IsValidPrakticniCsv(IEnumerable<BasicDTO> records);
        public bool IsValidUsmeniCsv(IEnumerable<BasicDTO> records);
        public string? ValidateCsvFile(IFormFile file);

        string? ValidateCsvContent(IEnumerable<BasicDTO> records, bool isUsmeni);
    }
}
