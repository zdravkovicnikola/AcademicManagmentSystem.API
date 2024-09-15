using System.ComponentModel.DataAnnotations;

namespace AcademicManagmentSystem.API.Core.Models.Predmeti
{
    public abstract class BasePredmetDto
    {
        [Required(ErrorMessage = "Naziv predmeta je obavezan.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Šifra predmeta je obavezna.")]
        public string Sifra { get; set; }

        [Range(1, 12, ErrorMessage = "ESPB mora biti između 1 i 12.")]
        public int ESPB { get; set; }
    }

}
