using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicManagmentSystem.API.Data
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Index { get; set; }

        public IList<Rezultat> Rezultati { get; set; }
    }
}
