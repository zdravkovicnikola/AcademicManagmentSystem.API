using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Repository
{
    public class TipRepository : GenericRepository<Tip>, ITipRepository
    {
        private readonly AcademicManagmentSystemDbContext _context;

        public TipRepository(AcademicManagmentSystemDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
