namespace RegistroServizi.Models.Entities
{
    public partial class SocioFamiliare
    {
        public SocioFamiliare()
        {
            SocioId = 0;
            Familiare = "N/A";
        }

        public int Id { get; private set; }
        public int SocioId { get; private set; }
        public string Familiare { get; private set; }
        public virtual Socio Socio { get; set; }

        public void ChangeSocioId(int socioId)
        {
            SocioId = socioId;
        }

        public void ChangeFamiliare(string newFamiliare)
        {
            if (string.IsNullOrWhiteSpace(newFamiliare))
            {
                newFamiliare = "N/A";
            }
            Familiare = newFamiliare;
        }
    }
}