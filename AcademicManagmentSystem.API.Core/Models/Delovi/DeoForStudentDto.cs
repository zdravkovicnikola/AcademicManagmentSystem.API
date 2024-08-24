using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Studenti;

namespace AcademicManagmentSystem.API.Models.Delovi
{
    public class DeoForStudentDto
    {
        public int DeoId { get; set; }
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public double MaxBrPoena { get; set; }
        public bool Polozio { get; set; }
        public string Napomena { get; set; }
        public Tip Tip { get; set; }
    }
}
