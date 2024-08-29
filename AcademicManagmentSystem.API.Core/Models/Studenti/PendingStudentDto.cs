using AcademicManagmentSystem.API.Core.Models.Delovi;

namespace AcademicManagmentSystem.API.Core.Models.Studenti
{
    public class PendingStudentDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Index { get; set; }

        public DeoForPendingStudentDto Deo { get; set; }
    }
}
