using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Deo
    {
        public int DeoId { get; set; }
        public DateTime Datum { get; set; }
        public double BrojPoena { get; set; }
        public double MaxBrPoena { get; set; }
        public bool Polozio { get; set; }
        public string Napomena { get; set; }

        [ForeignKey(nameof(PredmetId))]
        public int PredmetId { get; set; }
        public Predmet Predmet {get; set; }

        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        [ForeignKey(nameof(TipId))]
        public int TipId { get; set; }
        public Tip Tip { get; set; }

    }
}
