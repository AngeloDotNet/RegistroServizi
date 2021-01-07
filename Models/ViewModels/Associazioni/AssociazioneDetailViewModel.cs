using RegistroServizi.Models.Entities;

namespace RegistroServizi.Models.ViewModels.Associazioni
{
    public class AssociazioneDetailViewModel
    {
        public int Id { get; set; }
        public string Denominazione { get; set; }
        public string Sigla { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }

        public static AssociazioneDetailViewModel FromEntity(Associazione associazione)
        {
            return new AssociazioneDetailViewModel
            {
                Id = associazione.Id,
                Denominazione = associazione.Denominazione,
                Sigla = associazione.Sigla,
                Indirizzo = associazione.Indirizzo,
                Cap = associazione.Cap,
                Comune = associazione.Comune,
                Provincia = associazione.Provincia
            };
        }
    }
}