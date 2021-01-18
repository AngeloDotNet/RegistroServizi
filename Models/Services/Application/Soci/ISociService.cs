using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.Soci;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Soci;

namespace RegistroServizi.Models.Services.Application.Soci
{
    public interface ISociService
    {
        Task<ListViewModel<SocioViewModel>> GetSociAsync(SocioListInputModel model);
        Task<SocioEditInputModel> GetSocioForEditingAsync(int id);
        Task<SocioDetailViewModel> EditSocioAsync(SocioEditInputModel inputModel);
        Task<SocioDetailViewModel> GetSocioAsync(int id);
        Task<SocioDetailViewModel> CreateSocioAsync(SocioCreateInputModel inputModel);
        Task DeleteSocioAsync(SocioDeleteInputModel inputModel);
        Task<bool> IsSocioAvailableAsync(string nominativo, int excludeId);
        Task<bool> IsTesseraAvailableAsync(string tessera, int excludeId);
        Task<string> GetNextIdAsync();
    }
}