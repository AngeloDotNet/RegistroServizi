using RegistroServizi.Models.Enums;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.Entities
{
    public partial class Costo
    {
        public Costo()
        {
            TipoServizio = "N/A";
            CostoFisso = new Money(Currency.EUR, 0);
            CostoKm = new Money(Currency.EUR, 0);
            SecondoTrasportato = new Money(Currency.EUR, 0);
            FermoMacchina = new Money(Currency.EUR, 0);
            Accompagnatore = new Money(Currency.EUR, 0);
            ScontoSoci = 0;
        }

        public int Id { get; private set; }
        public string TipoServizio { get; private set; }
        public Money CostoFisso { get; private set; }
        public Money CostoKm { get; private set; }
        public Money SecondoTrasportato { get; private set; }
        public Money FermoMacchina { get; private set; }
        public Money Accompagnatore { get; private set; }
        public int ScontoSoci { get; private set; }

        public void ChangeTipoServizio(string newTipoServizio)
        {
            if (string.IsNullOrWhiteSpace(newTipoServizio))
            {
                newTipoServizio = "N/A";
            }
            TipoServizio = newTipoServizio;
        }

        public void ChangeCostoFisso(Money newCostoFisso)
        {
            if (newCostoFisso == null)
            {
                newCostoFisso = new Money(Currency.EUR, 0);
            }
            CostoFisso = newCostoFisso;
        }

        public void ChangeCostoKm(Money newCostoKm)
        {
            if (newCostoKm == null)
            {
                newCostoKm = new Money(Currency.EUR, 0);
            }
            CostoKm = newCostoKm;
        }

        public void ChangeSecondoTrasportato(Money newSecondoTrasportato)
        {
            if (newSecondoTrasportato == null)
            {
                newSecondoTrasportato = new Money(Currency.EUR, 0);
            }
            SecondoTrasportato = newSecondoTrasportato;
        }

        public void ChangeFermoMacchina(Money newFermoMacchina)
        {
            if (newFermoMacchina == null)
            {
                newFermoMacchina = new Money(Currency.EUR, 0);
            }
            FermoMacchina = newFermoMacchina;
        }

        public void ChangeAccompagnatore(Money newAccompagnatore)
        {
            if (newAccompagnatore == null)
            {
                newAccompagnatore = new Money(Currency.EUR, 0);
            }
            Accompagnatore = newAccompagnatore;
        }

        public void ChangeScontoSoci(int scontosoci)
        {
            ScontoSoci = scontosoci;
        }
    }
}