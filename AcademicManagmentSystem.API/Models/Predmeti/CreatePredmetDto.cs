using System.ComponentModel.DataAnnotations;

namespace AcademicManagmentSystem.API.Models.Predmeti
{
    public class CreatePredmetDto
    {
        [Required]
        public string Naziv { get; set; }
        public string Sifra { get; set; }
    }
}
