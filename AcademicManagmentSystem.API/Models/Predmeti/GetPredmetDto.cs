using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Predavaci;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Models.Predmeti
{
    public class GetPredmetDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
    }


    public class GetPredmetDetailsDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }

        public IList<GetPredmetPredavacDto> PredmetPredavaci { get; set; }

        public IList<GetDeoDto> Delovi { get; set; }

    }
    
}
