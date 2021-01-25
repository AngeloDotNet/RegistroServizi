using RegistroServizi.Models.Entities;
using RegistroServizi.Models.InputModels.Ospedali;
using RegistroServizi.Models.ViewModels.Ospedali;

namespace RegistroServizi.Models.Extensions
{
    public static class OspedaliExtension
    {
        public static OspedaleViewModel ToOspedaleViewModel(this Ospedale ospedale)
        {
            return new OspedaleViewModel
            {
                Id = ospedale.Id,
                Clinica = ospedale.Clinica,
                Comune = ospedale.Comune,
                Latitudine = ospedale.Latitudine,
                Longitudine = ospedale.Longitudine
            };
        }

        public static OspedaleDetailViewModel ToOspedaleDetailViewModel(this Ospedale ospedale)
        {
            return new OspedaleDetailViewModel
            {
                Id = ospedale.Id,
                Clinica = ospedale.Clinica,
                Comune = ospedale.Comune,
                Latitudine = ospedale.Latitudine,
                Longitudine = ospedale.Longitudine
            };
        }

        public static OspedaleEditInputModel ToOspedaleEditInputModel(this Ospedale ospedale)
        {
            return new OspedaleEditInputModel
            {
                Id = ospedale.Id,
                Clinica = ospedale.Clinica,
                Comune = ospedale.Comune,
                Latitudine = ospedale.Latitudine,
                Longitudine = ospedale.Longitudine
            };
        }
    }
}