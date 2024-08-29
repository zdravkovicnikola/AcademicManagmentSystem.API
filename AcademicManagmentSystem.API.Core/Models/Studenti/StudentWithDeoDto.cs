using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Delovi;

namespace AcademicManagmentSystem.API.Core.Models.Studenti
{
    public class StudentWithDeoDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Index { get; set; }

        public IEnumerable<DeoForStudentDto> Delovi { get; set; }
    }
}
