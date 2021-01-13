namespace RegistroServizi.Models.ViewModels.Clienti
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Comune { get; set; }
        public string Cap { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string PartitaIva { get; set; }
        public string CodiceFiscale { get; set; }
        public string Fax { get; set; }
        public int Spese { get; set; }
    }
}