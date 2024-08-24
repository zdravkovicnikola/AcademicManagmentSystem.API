using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Katedre;
using AcademicManagmentSystem.API.Models.Predavaci;

namespace AcademicManagmentSystem.API.Models.Predmeti
{
    public class UpdatePredmetDto : BasePredmetDto
    {
        public int predmetId { get; set; }
    }

    public class UpdatePredmetDetailsDto : BasePredmetDto
    {
        public int predmetId { get; set; }
        public IList<UpdatePredmetPredavacDto> PredmetPredavaci { get; set; }
    }


}
