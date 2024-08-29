using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Predavaci;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Core.Models.Predmeti
{
    public class GetPredmetDto : BasePredmetDto
    {
        public int PredmetId { get; set; }
    }


    public class GetPredmetDetailsDto : BasePredmetDto
    {
        public int PredmetId { get; set; }

        public IList<GetPredmetPredavacDto> PredmetPredavaci { get; set; }

        public IList<Tip> Delovi { get; set; }

    }
    
}
