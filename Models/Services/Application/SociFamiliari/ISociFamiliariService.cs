using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.SociFamiliari;
using RegistroServizi.Models.ViewModels.SociFamiliari;

namespace RegistroServizi.Models.Services.Application.SociFamiliari
{
    public interface ISociFamiliariService
    {
        Task<SocioFamiliareDetailViewModel> CreateSocioFamiliareAsync(SocioFamiliareCreateInputModel inputModel);
        Task<SocioFamiliareDetailViewModel> EditSocioFamiliareAsync(SocioFamiliareEditInputModel inputModel);
        Task<SocioFamiliareEditInputModel> GetSocioFamiliareForEditingAsync(int id);
        Task DeleteSocioFamiliareAsync(SocioFamiliareDeleteInputModel inputModel);
        Task<bool> IsSocioFamiliareAvailableAsync(string familiare, int excludeId);
    }
}