using AcademicManagmentSystem.API.Core.Models.Predmeti;

namespace AcademicManagmentSystem.API.Core.Models.Predavaci
{

    public class GetPredmetPredavacDto
    {
        public int PredavacId { get; set; }
        public GetPredavacDto Predavac { get; set; }

    }
}
