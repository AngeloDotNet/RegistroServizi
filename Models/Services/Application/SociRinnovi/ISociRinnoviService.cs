using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.SociRinnovi;
using RegistroServizi.Models.ViewModels.SociRinnovi;

namespace RegistroServizi.Models.Services.Application.SociRinnovi
{
    public interface ISociRinnoviService
    {
        Task<SocioRinnovoDetailViewModel> CreateSocioRinnovoAsync(SocioRinnovoCreateInputModel inputModel);
        Task<SocioRinnovoDetailViewModel> EditSocioRinnovoAsync(SocioRinnovoEditInputModel inputModel);
        Task<SocioRinnovoEditInputModel> GetSocioRinnovoForEditingAsync(int id);
        Task DeleteSocioRinnovoAsync(SocioRinnovoDeleteInputModel inputModel);
    }
}