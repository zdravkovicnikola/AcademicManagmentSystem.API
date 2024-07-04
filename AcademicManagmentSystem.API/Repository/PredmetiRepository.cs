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
        public async Task<Predmet> GetDetailsDeo(int id)
        {
            return await _context.Predmeti
                      .Include(p => p.Delovi)
                        .ThenInclude(pr => pr.Rezultati)
                            .ThenInclude(ps => ps.Student)
                                .FirstOrDefaultAsync(p => p.PredmetId == id); ;
        }
    }
}
