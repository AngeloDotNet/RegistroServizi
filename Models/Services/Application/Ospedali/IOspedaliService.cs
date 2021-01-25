using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.Ospedali;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Ospedali;

namespace RegistroServizi.Models.Services.Application.Ospedali
{
    public interface IOspedaliService
    {
        Task<ListViewModel<OspedaleViewModel>> GetOspedaliAsync(OspedaleListInputModel model);
        Task<OspedaleEditInputModel> GetOspedaleForEditingAsync(int id);
        Task<OspedaleDetailViewModel> EditOspedaleAsync(OspedaleEditInputModel inputModel);
        Task<OspedaleDetailViewModel> GetOspedaleAsync(int id);
        Task<OspedaleDetailViewModel> CreateOspedaleAsync(OspedaleCreateInputModel inputModel);
        Task<bool> IsOspedaleAvailableAsync(string clinica, int excludeId);
        Task DeleteOspedaleAsync(OspedaleDeleteInputModel inputModel);
    }
}