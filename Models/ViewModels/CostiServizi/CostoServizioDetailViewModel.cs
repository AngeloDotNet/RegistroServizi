using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.ViewModels.CostiServizi
{
    public class CostoServizioDetailViewModel
    {
        public int Id { get; set; }
        public string TipoServizio { get; set; }
        public Money CostoFisso { get; set; }
        public Money CostoKm { get; set; }
        public Money SecondoTrasportato { get; set; }
        public Money FermoMacchina { get; set; }
        public Money Accompagnatore { get; set; }
        public int ScontoSoci { get; set; }
    }
}