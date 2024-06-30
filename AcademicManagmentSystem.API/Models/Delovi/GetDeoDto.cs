using AcademicManagmentSystem.API.Models.Predmeti;
using AcademicManagmentSystem.API.Models.Rezultati;

namespace AcademicManagmentSystem.API.Models.Delovi
{

    public class GetDeoDto
    {
        public int DeoId { get; set; }
        public string Naziv { get; set; }

        public IList<GetRezultatDto> Rezultati { get; set; }
    }
}
