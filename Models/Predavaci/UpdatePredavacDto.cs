using AcademicManagmentSystem.API.Models.Katedre;

namespace AcademicManagmentSystem.API.Models.Predavaci
{
    public class UpdatePredavacDto
    {
        public int PredavacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }

        public int KatedraId { get; set; }
        public UpdateKatedraDto Katedra { get; set; }
    }
}
