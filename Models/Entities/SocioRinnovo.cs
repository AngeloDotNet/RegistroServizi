using RegistroServizi.Models.Enums;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.Entities
{
    public partial class SocioRinnovo
    {
        public SocioRinnovo()
        {
            SocioId = 0;
            Anno = "N/A";
            Quota = new Money(Currency.EUR, 0);
            DataRinnovo = "N/A";
        }

        public int Id { get; private set; }
        public int SocioId { get; private set; }
        public string Anno { get; private set; }
        public Money Quota { get; private set; }
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

        public void ChangeQuota(Money newQuota)
        {
            if (newQuota == null)
            {
                newQuota = new Money(Currency.EUR, 0);
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