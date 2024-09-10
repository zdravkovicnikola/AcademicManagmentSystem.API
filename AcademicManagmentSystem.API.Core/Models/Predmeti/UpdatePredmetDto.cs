using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Katedre;
using AcademicManagmentSystem.API.Core.Models.Predavaci;

namespace AcademicManagmentSystem.API.Core.Models.Predmeti
{
    public class UpdatePredmetDto : BasePredmetDto
    {

    }

    public class UpdatePredmetDetailsDto : BasePredmetDto
    {
        public IList<UpdatePredmetPredavacDto> PredmetPredavaci { get; set; }
    }


}
