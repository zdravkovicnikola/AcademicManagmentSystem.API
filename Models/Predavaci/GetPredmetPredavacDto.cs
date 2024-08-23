using AcademicManagmentSystem.API.Models.Predmeti;

namespace AcademicManagmentSystem.API.Models.Predavaci
{

    public class GetPredmetPredavacDto
    {
        public int PredavacId { get; set; }
        public GetPredavacDto Predavac { get; set; }

    }
}
