using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Ocena
    {
        [Key]
        public DateTime DatumPolaganja { get; set; }
        public int VrednostOcene { get; set; }

        [ForeignKey(nameof(PredmetId))]
        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }

        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
