using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            RagioneSociale = "N/A";
            Indirizzo = "N/A";
            Comune = "N/A";
            Cap = "N/A";
            Provincia = "N/A";
            Telefono = "N/A";
            PartitaIva = "N/A";
            CodiceFiscale = "N/A";
            Fax = "N/A";
            Spese = 0;
        }

        public int Id { get; private set; }
        public string RagioneSociale { get; private set; }
        public string Indirizzo { get; private set; }
        public string Comune { get; private set; }
        public string Cap { get; private set; }
        public string Provincia { get; private set; }
        public string Telefono { get; private set; }
        public string PartitaIva { get; private set; }
        public string CodiceFiscale { get; private set; }
        public string Fax { get; private set; }
        public int Spese { get; private set; }

        public void ChangeRagioneSociale(string newRagioneSociale)
        {
            if (string.IsNullOrWhiteSpace(newRagioneSociale))
            {
                newRagioneSociale = "N/A";
            }
            RagioneSociale = newRagioneSociale;
        }

        public void ChangeIndirizzo(string newIndirizzo)
        {
            if (string.IsNullOrWhiteSpace(newIndirizzo))
            {
                newIndirizzo = "N/A";
            }
            Indirizzo = newIndirizzo;
        }

        public void ChangeComune(string newComune)
        {
            if (string.IsNullOrWhiteSpace(newComune))
            {
                newComune = "N/A";
            }
            Comune = newComune;
        }

        public void ChangeCap(string newCap)
        {
            if (string.IsNullOrWhiteSpace(newCap))
            {
                newCap = "N/A";
            }
            Cap = newCap;
        }

        public void ChangeProvincia(string newProvincia)
        {
            if (string.IsNullOrWhiteSpace(newProvincia))
            {
                newProvincia = "N/A";
            }
            Provincia = newProvincia;
        }

        public void ChangeTelefono(string newTelefono)
        {
            if (string.IsNullOrWhiteSpace(newTelefono))
            {
                newTelefono = "N/A";
            }
            Telefono = newTelefono;
        }

        public void ChangePartitaIva(string newPartitaIva)
        {
            if (string.IsNullOrWhiteSpace(newPartitaIva))
            {
                newPartitaIva = "N/A";
            }
            PartitaIva = newPartitaIva;
        }

        public void ChangeCodiceFiscale(string newCodiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(newCodiceFiscale))
            {
                newCodiceFiscale = "N/A";
            }
            CodiceFiscale = newCodiceFiscale;
        }

        public void ChangeFax(string newFax)
        {
            if (string.IsNullOrWhiteSpace(newFax))
            {
                newFax = "N/A";
            }
            Fax = newFax;
        }

        public void ChangeSpese(int spese)
        {
            Spese = spese;
        }
    }
}