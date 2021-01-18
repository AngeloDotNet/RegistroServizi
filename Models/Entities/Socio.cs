using System.Collections.Generic;

namespace RegistroServizi.Models.Entities
{
    public partial class Socio
    {
        public Socio()
        {
            Tessera = "N/A";
            Nominativo = "N/A";
            Indirizzo = "N/A";
            Cap = "N/A";
            Comune = "N/A";
            Provincia = "N/A";
            LuogoNascita = "N/A";
            DataNascita = "N/A";
            CodiceFiscale = "N/A";
            Telefono = "N/A";
            Email = "N/A";
            DataTesseramento = "N/A";
            TrattamentoDati = "N/A";
            Professione = "N/A";
            Zona = "N/A";
        }

        public int Id { get; private set; }
        public string Tessera { get; private set; }
        public string Nominativo { get; private set; }
        public string Indirizzo { get; private set; }
        public string Cap { get; private set; }
        public string Comune { get; private set; }
        public string Provincia { get; private set; }
        public string LuogoNascita { get; private set; }
        public string DataNascita { get; private set; }
        public string CodiceFiscale { get; private set; }
        public string Telefono { get; private set; }
        public string Email { get; private set; }
        public string DataTesseramento { get; private set; }
        public string TrattamentoDati { get; private set; }
        public string Professione { get; private set; }
        public string Zona { get; private set; }

        public virtual ICollection<SocioFamiliare> SociFamiliari { get; private set; }
        public virtual ICollection<SocioRinnovo> SociRinnovi { get; private set; }

        public void ChangeTessera(string newTessera)
        {
            if (string.IsNullOrWhiteSpace(newTessera))
            {
                newTessera = "N/A";
            }
            Tessera = newTessera;
        }

        public void ChangeNominativo(string newNominativo)
        {
            if (string.IsNullOrWhiteSpace(newNominativo))
            {
                newNominativo = "N/A";
            }
            Nominativo = newNominativo;
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

        public void ChangeLuogoNascita(string newLuogoNascita)
        {
            if (string.IsNullOrWhiteSpace(newLuogoNascita))
            {
                newLuogoNascita = "N/A";
            }
            LuogoNascita = newLuogoNascita;
        }

        public void ChangeDataNascita(string newDataNascita)
        {
            if (string.IsNullOrWhiteSpace(newDataNascita))
            {
                newDataNascita = "N/A";
            }
            DataNascita = newDataNascita;
        }

        public void ChangeCodiceFiscale(string newCodiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(newCodiceFiscale))
            {
                newCodiceFiscale = "N/A";
            }
            CodiceFiscale = newCodiceFiscale;
        }

        public void ChangeTelefono(string newTelefono)
        {
            if (string.IsNullOrWhiteSpace(newTelefono))
            {
                newTelefono = "N/A";
            }
            Telefono = newTelefono;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                newEmail = "N/A";
            }
            Email = newEmail;
        }

        public void ChangeDataTesseramento(string newDataTesseramento)
        {
            if (string.IsNullOrWhiteSpace(newDataTesseramento))
            {
                newDataTesseramento = "N/A";
            }
            DataTesseramento = newDataTesseramento;
        }

        public void ChangeTrattamentoDati(string newTrattamentoDati)
        {
            if (string.IsNullOrWhiteSpace(newTrattamentoDati))
            {
                newTrattamentoDati = "N/A";
            }
            TrattamentoDati = newTrattamentoDati;
        }

        public void ChangeProfessione(string newProfessione)
        {
            if (string.IsNullOrWhiteSpace(newProfessione))
            {
                newProfessione = "N/A";
            }
            Professione = newProfessione;
        }

        public void ChangeZona(string newZona)
        {
            if (string.IsNullOrWhiteSpace(newZona))
            {
                newZona = "N/A";
            }
            Zona = newZona;
        }
    }
}