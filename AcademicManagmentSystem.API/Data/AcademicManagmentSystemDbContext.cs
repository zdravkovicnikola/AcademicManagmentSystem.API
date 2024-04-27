using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Data
{
    public class AcademicManagmentSystemDbContext:DbContext
    {
        public AcademicManagmentSystemDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
