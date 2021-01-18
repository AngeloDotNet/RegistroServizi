using RegistroServizi.Models.Entities;
using RegistroServizi.Models.ViewModels.SociFamiliari;

namespace RegistroServizi.Models.Extensions
{
    public static class SociFamiliariExtension
    {
        public static SocioFamiliareViewModel ToSocioFamiliareViewModel(this SocioFamiliare sociofamiliare)
        {
            return new SocioFamiliareViewModel
            {
                Id = sociofamiliare.Id,
                SocioId = sociofamiliare.SocioId,
                Familiare = sociofamiliare.Familiare
            };
        }

        public static SocioFamiliareDetailViewModel ToSocioFamiliareDetailViewModel(this SocioFamiliare sociofamiliare)
        {
            return new SocioFamiliareDetailViewModel
            {
                Id = sociofamiliare.Id,
                SocioId = sociofamiliare.SocioId,
                Familiare = sociofamiliare.Familiare
            };
        }
    }
}