using RegistroServizi.Models.InputModels.Missioni;

namespace RegistroServizi.Models.Services.Application.Missioni
{
    public interface IMissioniService
    {
        MissioneEditViewModel CreateMissioneAsync(MissioneCreateInputModel inputModel);
        MissioneEditViewModel GetMissioneForEditingAsync(string missionId);
        IList<MissioneViewModel> GetMissioniAsync();
    }
}