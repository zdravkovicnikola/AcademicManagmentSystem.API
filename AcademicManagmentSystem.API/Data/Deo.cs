using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Deo
    {
        public int DeoId { get; set; }
        public string Naziv { get; set; }

        [ForeignKey(nameof(PredmetId))]
        public int PredmetId { get; set; }
        public Predmet Predmet {get; set; }

        public IList<Rezultat> Rezultati { get; set; }
    }
}
