using System.ComponentModel.DataAnnotations;

namespace AcademicManagmentSystem.API.Models.Predmeti
{
    public abstract class BasePredmetDto
    {
        [Required]
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public int ESPB { get; set; }

    }
}
