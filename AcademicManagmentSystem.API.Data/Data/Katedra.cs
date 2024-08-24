namespace AcademicManagmentSystem.API.Data
{
    public class Katedra
    {
        public int KatedraID { get; set; }
        public string Naziv { get; set; }

        public IList<Predavac> Predavaci { get; set; }

    }
}
