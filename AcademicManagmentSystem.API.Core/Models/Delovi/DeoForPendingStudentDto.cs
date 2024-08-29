namespace AcademicManagmentSystem.API.Core.Models.Delovi
{
    public class DeoForPendingStudentDto
    {
        public int DeoId { get; set; }
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public double MaxBrPoena { get; set; } = 100;
        public bool Polozio { get; set; }
        public string Napomena { get; set; }
    }
}
