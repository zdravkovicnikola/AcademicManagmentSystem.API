using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Studenti;

namespace AcademicManagmentSystem.API.Core.Models.Delovi
{
    public class DeoForStudentDto
    {
        public int DeoId { get; set; }
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public double MaxBrPoena { get; set; } = 100;
        public bool Polozio { get; set; }
        public string Napomena { get; set; }
        public Tip Tip { get; set; }
    }
}
