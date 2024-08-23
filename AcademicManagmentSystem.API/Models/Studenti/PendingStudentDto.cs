using AcademicManagmentSystem.API.Models.Delovi;

namespace AcademicManagmentSystem.API.Models.Studenti
{
    public class PendingStudentDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Index { get; set; }

        public DeoForPendingStudentDto Deo { get; set; }
    }
}
