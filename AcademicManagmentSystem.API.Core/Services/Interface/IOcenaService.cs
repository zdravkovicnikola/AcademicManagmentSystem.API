using AcademicManagmentSystem.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface IOcenaService
    {
        Task<int> IzracunajOcenuZaStudenta(Student student, int predmetId);
    }
}
