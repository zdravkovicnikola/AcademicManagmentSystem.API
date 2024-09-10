using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Repository
{
    public class PredmetiRepository : GenericRepository<Predmet>, IPredmetiRepository
    {
        private readonly AcademicManagmentSystemDbContext _context;

        public PredmetiRepository(AcademicManagmentSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Predmet> GetDetailsPredavac(int id)
        {
            return await _context.Predmeti
                       .Include(p => p.PredmetPredavaci)
                           .ThenInclude(pp => pp.Predavac)
                               .ThenInclude(pr => pr.Katedra)
                                .FirstOrDefaultAsync(p => p.PredmetId == id);
        }

        public async Task<Predmet> GetAsyncByPass(string sifra)
        {
            if (string.IsNullOrWhiteSpace(sifra))
                return null;

            return await _context.Predmeti
                .FirstOrDefaultAsync(p => p.Sifra == sifra);
        }

        public async Task<int> GetBrojTipovaDelova(int predmetId)
        {
            return await _context.Predmeti
                .Include(p => p.Delovi)
                .ThenInclude(d => d.Tip)
                .Where(p => p.PredmetId == predmetId)
                .SelectMany(p => p.Delovi.Select(d => d.Tip.TipId))
                .Distinct()
                .CountAsync(); 
        }
    }
}
