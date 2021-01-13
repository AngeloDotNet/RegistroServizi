using RegistroServizi.Models.Entities;
using RegistroServizi.Models.InputModels.Clienti;
using RegistroServizi.Models.ViewModels.Clienti;

namespace RegistroServizi.Models.Extensions
{
    public static class ClientiExtension
    {
        public static ClienteViewModel ToClienteViewModel(this Cliente cliente)
        {
            return new ClienteViewModel
            {
                Id = cliente.Id,
                RagioneSociale = cliente.RagioneSociale,
                Indirizzo = cliente.Indirizzo,
                Comune = cliente.Comune,
                Cap = cliente.Cap,
                Provincia = cliente.Provincia,
                Telefono = cliente.Telefono,
                PartitaIva = cliente.PartitaIva,
                CodiceFiscale = cliente.CodiceFiscale,
                Fax = cliente.Fax,
                Spese = cliente.Spese
            };
        }

        public static ClienteDetailViewModel ToClienteDetailViewModel(this Cliente cliente)
        {
            return new ClienteDetailViewModel
            {
                Id = cliente.Id,
                RagioneSociale = cliente.RagioneSociale,
                Indirizzo = cliente.Indirizzo,
                Comune = cliente.Comune,
                Cap = cliente.Cap,
                Provincia = cliente.Provincia,
                Telefono = cliente.Telefono,
                PartitaIva = cliente.PartitaIva,
                CodiceFiscale = cliente.CodiceFiscale,
                Fax = cliente.Fax,
                Spese = cliente.Spese
            };
        }

        public static ClienteEditInputModel ToClienteEditInputModel(this Cliente cliente)
        {
            return new ClienteEditInputModel
            {
                Id = cliente.Id,
                RagioneSociale = cliente.RagioneSociale,
                Indirizzo = cliente.Indirizzo,
                Comune = cliente.Comune,
                Cap = cliente.Cap,
                Provincia = cliente.Provincia,
                Telefono = cliente.Telefono,
                PartitaIva = cliente.PartitaIva,
                CodiceFiscale = cliente.CodiceFiscale,
                Fax = cliente.Fax,
                Spese = cliente.Spese
            };
        }
    }
}