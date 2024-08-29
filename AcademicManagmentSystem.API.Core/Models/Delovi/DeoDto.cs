using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Studenti;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Core.Models.Delovi
{
    public class CreateDeoDto
    {
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public int PredmetId { get; set; }
    }

    public class UpdateDeoDto
    {
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public int PredmetId { get; set; }
    }

    public class DeoDto
    {
        public int DeoId { get; set; }
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public double MaxBrPoena { get; set; } = 100;
        public bool Polozio { get; set; }
        public string Napomena { get; set; }

        public GetStudentDto Student { get; set; }
        public Tip Tip { get; set; }

    }

}
