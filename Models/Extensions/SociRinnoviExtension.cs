using RegistroServizi.Models.Entities;
using RegistroServizi.Models.ViewModels.SociRinnovi;

namespace RegistroServizi.Models.Extensions
{
    public static class SociRinnoviExtension
    {
        public static SocioRinnovoViewModel ToSocioRinnovoViewModel(this SocioRinnovo sociorinnovo)
        {
            return new SocioRinnovoViewModel
            {
                Id = sociorinnovo.Id,
                SocioId = sociorinnovo.SocioId,
                Anno = sociorinnovo.Anno,
                Quota = sociorinnovo.Quota,
                DataRinnovo = sociorinnovo.DataRinnovo
            };
        }

        public static SocioRinnovoDetailViewModel ToSocioRinnovoDetailViewModel(this SocioRinnovo sociorinnovo)
        {
            return new SocioRinnovoDetailViewModel
            {
                Id = sociorinnovo.Id,
                SocioId = sociorinnovo.SocioId,
                Anno = sociorinnovo.Anno,
                Quota = sociorinnovo.Quota,
                DataRinnovo = sociorinnovo.DataRinnovo
            };
        }
    }
}