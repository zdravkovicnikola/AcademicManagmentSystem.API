using AcademicManagmentSystem.API.Models;
using AcademicManagmentSystem.API.Services.Interface;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AcademicManagmentSystem.API.Services.Implementation
{
    public class CsvService : ICsvService
    {
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
                    flag++;
                    var record = new BasicDTO();
                    for (var i = 0; csv.TryGetField<string>(i, out var field); i++)
                    {
                        if (field == null) break;
                        record.Data.Add(field);
                    }
                    if (flag != 1)
                        records.Add(record);
                }
            }

            return records;
        }
    }
}
