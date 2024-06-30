using AcademicManagmentSystem.API.Models.Predmeti;
using AcademicManagmentSystem.API.Models.Studenti;

namespace AcademicManagmentSystem.API.Models.Rezultati
{
    public class GetRezultatDto
    {
        public int RezultatId { get; set; }
        public double Poeni { get; set; }
        public int Ocena { get; set; } // Ocena studenta za određeni predmet
        public DateTime Datum { get; set; }

        public int StudentId { get; set; }
        public GetStudentDto Student { get; set; }
    }
}
