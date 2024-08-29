using AcademicManagmentSystem.API.Core.Models.Katedre;
using AcademicManagmentSystem.API.Core.Models.Predmeti;

namespace AcademicManagmentSystem.API.Core.Models.Predavaci
{
    public class GetPredavacDto
    {
        public int PredavacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }

        public int KatedraId { get; set; }
        public GetKatedraDto Katedra { get; set; }
    }
}
