using RegistroServizi.Models.Entities;
using RegistroServizi.Models.InputModels.CostiServizi;
using RegistroServizi.Models.ViewModels.CostiServizi;

namespace RegistroServizi.Models.Extensions
{
    public static class CostiServiziExtension
    {
        public static CostoServizioViewModel ToCostoServizioViewModel(this Costo costo)
        {
            return new CostoServizioViewModel
            {
                Id = costo.Id,
                TipoServizio = costo.TipoServizio,
                CostoFisso = costo.CostoFisso,
                CostoKm = costo.CostoKm,
                SecondoTrasportato = costo.SecondoTrasportato,
                FermoMacchina = costo.FermoMacchina,
                Accompagnatore = costo.Accompagnatore,
                ScontoSoci = costo.ScontoSoci
            };
        }

        public static CostoServizioDetailViewModel ToCostoServizioDetailViewModel(this Costo costo)
        {
            return new CostoServizioDetailViewModel
            {
                Id = costo.Id,
                TipoServizio = costo.TipoServizio,
                CostoFisso = costo.CostoFisso,
                CostoKm = costo.CostoKm,
                SecondoTrasportato = costo.SecondoTrasportato,
                FermoMacchina = costo.FermoMacchina,
                Accompagnatore = costo.Accompagnatore,
                ScontoSoci = costo.ScontoSoci
            };
        }

        public static CostoServizioEditInputModel ToCostoServizioEditInputModel(this Costo costo)
        {
            return new CostoServizioEditInputModel
            {
                Id = costo.Id,
                TipoServizio = costo.TipoServizio,
                CostoFisso = costo.CostoFisso,
                CostoKm = costo.CostoKm,
                SecondoTrasportato = costo.SecondoTrasportato,
                FermoMacchina = costo.FermoMacchina,
                Accompagnatore = costo.Accompagnatore,
                ScontoSoci = costo.ScontoSoci
            };
        }
    }
}