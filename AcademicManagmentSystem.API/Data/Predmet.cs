using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Predmet
    {
        public int PredmetId { get; set; }
        public string Naziv { get; set; }
        public string Sifra  { get; set; }

        public IList<PredmetPredavac> PredmetPredavaci { get; set; }

        public IList<Deo> Delovi { get; set; }
    }
}