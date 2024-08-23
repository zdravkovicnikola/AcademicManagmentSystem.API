namespace AcademicManagmentSystem.API.Models.Ocena
{
    public class CreateOcenaDto
    {
        public DateTime DatumPolaganja { get; set; }
        public int VrednostOcene { get; set; }
        public int PredmetId { get; set; }
        public int StudentId { get; set; }
    }

}