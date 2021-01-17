namespace RegistroServizi.Models.Entities
{
    public partial class SocioRinnovo
    {
        public SocioRinnovo()
        {
            SocioId = 0;
            Anno = "N/A";
            Quota = "N/A";
            DataRinnovo = "N/A";
        }

        public int Id { get; private set; }
        public int SocioId { get; private set; }
        public string Anno { get; private set; }
        public string Quota { get; private set; }
        public string DataRinnovo { get; private set; }
        public virtual Socio Socio { get; set; }

        public void ChangeSocioId(int socioId)
        {
            SocioId = socioId;
        }

        public void ChangeAnno(string newAnno)
        {
            if (string.IsNullOrWhiteSpace(newAnno))
            {
                newAnno = "N/A";
            }
            Anno = newAnno;
        }

        public void ChangeQuota(string newQuota)
        {
            if (string.IsNullOrWhiteSpace(newQuota))
            {
                newQuota = "N/A";
            }
            Quota = newQuota;
        }

        public void ChangeDataRinnovo(string newDataRinnovo)
        {
            if (string.IsNullOrWhiteSpace(newDataRinnovo))
            {
                newDataRinnovo = "N/A";
            }
            DataRinnovo = newDataRinnovo;
        }
    }
}