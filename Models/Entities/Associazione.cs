namespace RegistroServizi.Models.Entities
{
    public partial class Associazione
    {
        public Associazione()
        {
            Denominazione = "N/A";
            Sigla = "N/A";
            Indirizzo = "N/A";
            Cap = "N/A";
            Comune = "N/A";
            Provincia = "N/A";
        }

        public int Id { get; private set; }
        public string Denominazione { get; private set; }
        public string Sigla { get; private set; }
        public string Indirizzo { get; private set; }
        public string Cap { get; private set; }
        public string Comune { get; private set; }
        public string Provincia { get; private set; }

        public void ChangeDenominazione(string newDenominazione)
        {
            if (string.IsNullOrWhiteSpace(newDenominazione))
            {
                newDenominazione = "N/A";
            }
            Denominazione = newDenominazione;
        }

        public void ChangeSigla(string newSigla)
        {
            if (string.IsNullOrWhiteSpace(newSigla))
            {
                newSigla = "N/A";
            }
            Sigla = newSigla;
        }

        public void ChangeIndirizzo(string newIndirizzo)
        {
            if (string.IsNullOrWhiteSpace(newIndirizzo))
            {
                newIndirizzo = "N/A";
            }
            Indirizzo = newIndirizzo;
        }

        public void ChangeCap(string newCap)
        {
            if (string.IsNullOrWhiteSpace(newCap))
            {
                newCap = "N/A";
            }
            Cap = newCap;
        }

        public void ChangeComune(string newComune)
        {
            if (string.IsNullOrWhiteSpace(newComune))
            {
                newComune = "N/A";
            }
            Comune = newComune;
        }

        public void ChangeProvincia(string newProvincia)
        {
            if (string.IsNullOrWhiteSpace(newProvincia))
            {
                newProvincia = "N/A";
            }
            Provincia = newProvincia;
        }
    }
}