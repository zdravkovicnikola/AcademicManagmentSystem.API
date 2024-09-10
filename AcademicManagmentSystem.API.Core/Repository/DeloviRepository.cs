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

        public async Task<IEnumerable<Deo>> GetAllResultsForSubjectAsync(int predmetId, DateTime datum, int tipId)
        {
            return await _context.Delovi
                .Include(d => d.Student)
                .Include(d => d.Tip)
                .Where(d => d.PredmetId == predmetId && d.Datum.Date == datum.Date && d.TipId == tipId)
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
