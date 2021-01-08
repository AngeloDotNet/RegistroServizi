using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.CostiServizi;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.CostiServizi;

namespace RegistroServizi.Models.Services.Application.CostiServizi
{
    public interface ICostiServiziService
    {
        Task<ListViewModel<CostoServizioViewModel>> GetCostiServiziAsync(CostoServizioListInputModel model);
        Task<CostoServizioEditInputModel> GetCostoServizioForEditingAsync(int id);
        Task<CostoServizioDetailViewModel> EditCostoServizioAsync(CostoServizioEditInputModel inputModel);
        Task<CostoServizioDetailViewModel> GetCostoServizioAsync(int id);
    }
}