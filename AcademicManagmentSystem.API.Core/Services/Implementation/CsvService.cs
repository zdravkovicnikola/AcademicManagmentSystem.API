using AcademicManagmentSystem.API.Core.Models;
using AcademicManagmentSystem.API.Core.Services.Interface;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class CsvService : ICsvService
    {

        public CsvService()
        {
            
        } 
        public bool IsValidPrakticniCsv(IEnumerable<BasicDTO> records)
        {
            var firstRecord = records.FirstOrDefault();
            if (firstRecord == null) return false;

            return firstRecord.Data.Contains("Ime") && 
                firstRecord.Data.Contains("Prezime") && 
                firstRecord.Data.Contains("Godina") &&
                firstRecord.Data.Contains("Broj") &&
                firstRecord.Data.Contains("Kompajlira") &&
                firstRecord.Data.Contains("Ukupno");
        }

        public bool IsValidUsmeniCsv(IEnumerable<BasicDTO> records)
        {
            var firstRecord = records.FirstOrDefault();
            if (firstRecord == null) return false;

            return firstRecord.Data.Contains("Презиме") &&
                firstRecord.Data.Contains("Име") &&
                firstRecord.Data.Contains("Адреса е-поште") &&
                firstRecord.Data.Contains("Статус") &&
                firstRecord.Data.Contains("Започето") &&
                firstRecord.Data.Contains("Оцена/60,00");
        }

        public IEnumerable<BasicDTO> LoadCsvData(string filePath)
        {
            var records = new List<BasicDTO>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                HeaderValidated = null
            };
            int flag = 0;
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {

                    var record = new BasicDTO();
                    for (var i = 0; csv.TryGetField<string>(i, out var field); i++)
                    {
                        if (field == null) break;
                        record.Data.Add(field);
                    }
                    records.Add(record);
                }
            }

            return records;
        }


        public string? ValidateCsvContent(IEnumerable<BasicDTO> records, bool isUsmeni)
        {
            if (records == null || !records.Any())
            {
                return "CSV fajl je prazan ili nevazeci.";
            }

            if (isUsmeni && !IsValidUsmeniCsv(records))
            {
                return "Ovo nije validan format za usmeni deo ispita.";
            } else if (!isUsmeni && !IsValidPrakticniCsv(records))
            {
                return "Ovo nije validan format za prakticni deo ispita.";
            }

            return null; 
        }

        public string? ValidateCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "Invalid file.";
            }

            var contentType = file.ContentType;
            if (contentType != "text/csv" && contentType != "application/vnd.ms-excel")
            {
                return "Dozvoljeni su samo CSV fajlovi.";
            }

            return null; 
        }

    }
}
