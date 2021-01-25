namespace RegistroServizi.Models.Entities
{
    public partial class Ospedale
    {
        public Ospedale()
        {
            Clinica = "N/A";
            Comune = "N/A";
            Latitudine = "N/A";
            Longitudine = "N/A";
        }

        public int Id { get; private set; }
        public string Clinica { get; private set; }
        public string Comune { get; private set; }
        public string Latitudine { get; private set; }
        public string Longitudine { get; private set; }

        public void ChangeClinica(string newClinica)
        {
            if (string.IsNullOrWhiteSpace(newClinica))
            {
                newClinica = "N/A";
            }
            Clinica = newClinica;
        }

        public void ChangeComune(string newComune)
        {
            if (string.IsNullOrWhiteSpace(newComune))
            {
                newComune = "N/A";
            }
            Comune = newComune;
        }

        public void ChangeLatitudine(string newLatitudine)
        {
            if (string.IsNullOrWhiteSpace(newLatitudine))
            {
                newLatitudine = "N/A";
            }
            Latitudine = newLatitudine;
        }

        public void ChangeLongitudine(string newLongitudine)
        {
            if (string.IsNullOrWhiteSpace(newLongitudine))
            {
                newLongitudine = "N/A";
            }
            Longitudine = newLongitudine;
        }
    }
}