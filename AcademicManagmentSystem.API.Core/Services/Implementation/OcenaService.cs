using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Core.Services.Interface;
using AcademicManagmentSystem.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class OcenaService : IOcenaService
    {
        private readonly IPredmetiRepository _predmetiRepository;

        public OcenaService(IPredmetiRepository predmetiRepository)
        {

            _predmetiRepository = predmetiRepository;
        }

        public async Task<int> IzracunajOcenuZaStudenta(Student student, int predmetId)
        {
            var deloviZaPredmet = student.Delovi
            .Where(d => d.PredmetId == predmetId)
            .GroupBy(d => d.TipId)
            .Select(g => g.OrderByDescending(d => d.Datum).FirstOrDefault())
            .ToList();

            var brojPotrebnihDelova = await _predmetiRepository.GetBrojTipovaDelova(predmetId);

            if (deloviZaPredmet.Count != brojPotrebnihDelova || deloviZaPredmet.Any(d => d.BrojPoena <= 51))
            {
                return 5;
            }

            var prosecniPoeni = deloviZaPredmet.Average(d => d.BrojPoena);

            if (prosecniPoeni >= 91) return 10;
            if (prosecniPoeni >= 81) return 9;
            if (prosecniPoeni >= 71) return 8;
            if (prosecniPoeni >= 61) return 7;
            if (prosecniPoeni >= 51) return 6;

            return 5;
        }
    }
}
