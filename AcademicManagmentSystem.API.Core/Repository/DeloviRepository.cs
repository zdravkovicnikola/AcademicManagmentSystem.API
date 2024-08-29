using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Delovi;
using Microsoft.EntityFrameworkCore;
using Deo = AcademicManagmentSystem.API.Data.Deo;

namespace AcademicManagmentSystem.API.Repository
{
    public class DeloviRepository : GenericRepository<Deo>, IDeloviRepository
    {
        private readonly AcademicManagmentSystemDbContext _context;
        public DeloviRepository(AcademicManagmentSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deo>> GetAllResultsForStudentAsync(int predmetId, int studentId)
        {
            return await _context.Delovi.Include(d => d.Tip).Where(d => d.PredmetId == predmetId && d.StudentId == studentId).ToListAsync();
        }

        public async Task<IEnumerable<Deo>> GetAllResultsForSubjectAsync(int predmetId)
        {
            return await _context.Delovi
                                        .Include(d => d.Student). Include(d => d.Tip)
                                        .Where(d => d.PredmetId == predmetId)
                                        .GroupBy(d => new { d.StudentId, d.TipId })
                                        .Select(g => g.OrderByDescending(d => d.Datum).FirstOrDefault())
                                        .ToListAsync();
        }

        public async Task<IList<Tip>> GetTipoviForSubject(int predmetId)
        {
            return await _context.Delovi
                                        .Include(d => d.Tip)
                                        .Where(d => d.PredmetId == predmetId)
                                        .Select(d => d.Tip)
                                        .Distinct()
                                        .ToListAsync();
        }
    }
}
