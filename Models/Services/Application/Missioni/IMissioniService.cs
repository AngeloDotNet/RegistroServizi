using System.Collections.Generic;
using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.Missioni;
using RegistroServizi.Models.ViewModels.Missioni;

namespace RegistroServizi.Models.Services.Application.Missioni
{
    public interface IMissioniService
    {
        Task<MissioneViewModel> CreateMissioneAsync(MissioneCreateInputModel inputModel);
        Task<MissioneEditInputModel> GetMissioneForEditingAsync(int idMissione);
        Task<MissioneViewModel> EditMissionAsync(MissioneEditInputModel inputModel);
        Task RefreshEditingMissioneAsync(int idMissione);
        Task<IList<MissioneViewModel>> GetMissioniAsync();
    }
}