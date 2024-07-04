using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Predavaci;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Models.Predmeti
{
    public class GetPredmetDto : BasePredmetDto
    {
        public int PredmetId { get; set; }
    }


    public class GetPredmetDetailsDto : BasePredmetDto
    {
        public int PredmetId { get; set; }

        public IList<GetPredmetPredavacDto> PredmetPredavaci { get; set; }

        public IList<GetDeoDto> Delovi { get; set; }

    }
    
}
