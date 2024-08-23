using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Studenti;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Models.Delovi
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
        public double MaxBrPoena { get; set; }
        public bool Polozio { get; set; }
        public string Napomena { get; set; }

        public GetStudentDto Student { get; set; }
        public Tip Tip { get; set; }

    }

}
