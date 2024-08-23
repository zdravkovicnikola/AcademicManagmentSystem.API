using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AcademicManagmentSystemDbContext _context;

        public StudentRepository(AcademicManagmentSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetAsyncByIndex(string? indeks)
        {
            if (string.IsNullOrWhiteSpace(indeks))
                return null;

            return await _context.Studenti
                .FirstOrDefaultAsync(s => s.Index == indeks);
        }

        public async Task<Student> GetAsyncStudent(string? index)
        {
            if (index is null)
                return null;

            return await _context.Set<Student>()
                .Include(s => s.Ocene)
                .Include(s => s.Delovi)
                    .ThenInclude(d => d.Tip) 
                .Include(s => s.Delovi)
                    .ThenInclude(d => d.Predmet) 
                .FirstOrDefaultAsync(s => s.Index == index);
        }

        public async Task<IEnumerable<Student>> GetStudentsByPredmetId(int predmetId, DateTime startDate, DateTime endDate)
        {
            return await _context.Studenti
                .Include(s => s.Delovi)
                .ThenInclude(d => d.Tip) 
                .Where(s => s.Delovi.Any(d => d.PredmetId == predmetId && d.Polozio && d.Datum > startDate && d.Datum < endDate)) // Filtriramo studente koji imaju deo za dati predmet
                .Select(s => new Student
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Index = s.Index,
                    Delovi = s.Delovi
                        .Where(d => d.PredmetId == predmetId && d.Polozio && d.Datum > startDate && d.Datum < endDate) // Filtriramo delove koji pripadaju datom predmetu
                        .GroupBy(d => new { d.StudentId, d.TipId })
                        .Select(g => g.OrderByDescending(d => d.Datum).FirstOrDefault())
                        .ToList()
                })
                .ToListAsync();
        }
    }
}
