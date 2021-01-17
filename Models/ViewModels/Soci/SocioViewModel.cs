namespace RegistroServizi.Models.ViewModels.Soci
{
    public class SocioViewModel
    {
        public int Id { get; set; }
        public string Tessera { get; set; }
        public string Nominativo { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string LuogoNascita { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DataTesseramento { get; set; }
        public string TrattamentoDati { get; set; }
        public string Professione { get; set; }
        public string Zona { get; set; }   
    }
}