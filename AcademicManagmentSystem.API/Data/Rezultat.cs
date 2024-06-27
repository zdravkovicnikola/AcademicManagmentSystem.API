using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Rezultat
    {
        public int RezultatId { get; set; }
        public double Poeni { get; set; } 
        public int Ocena { get; set; } // Ocena studenta za određeni predmet
        public DateTime Datum { get; set; }


        [ForeignKey(nameof(DeoId))]
        public int DeoId { get; set; }
        public Deo Deo { get; set; }

        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
