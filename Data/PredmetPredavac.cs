namespace AcademicManagmentSystem.API.Data
{
    public class PredmetPredavac
    {
        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }

        public int PredavacId { get; set; }
        public Predavac Predavac { get; set; }
    }
}