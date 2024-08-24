using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;

namespace AcademicManagmentSystem.API.Models.Studenti
{
    public class StudentWithDeoDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Index { get; set; }

        public IEnumerable<DeoForStudentDto> Delovi { get; set; }
    }
}
