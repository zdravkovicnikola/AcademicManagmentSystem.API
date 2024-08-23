using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Predavac
    {
        public int PredavacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        [ForeignKey(nameof(KatedraId))]
        public int KatedraId { get; set; }
        public Katedra Katedra { get; set; }

        public IList<PredmetPredavac> PredmetPredavaci { get; set; }
    }
}
