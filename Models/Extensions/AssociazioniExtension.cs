using RegistroServizi.Models.Entities;
using RegistroServizi.Models.ViewModels.Associazioni;

namespace RegistroServizi.Models.Extensions
{
    public static class AssociazioniExtension
    {
        public static AssociazioneViewModel ToAssociazioneViewModel(this Associazione associazione)
        {
            return new AssociazioneViewModel
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

        public static AssociazioneDetailViewModel ToAssociazioneDetailViewModel(this Associazione associazione)
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