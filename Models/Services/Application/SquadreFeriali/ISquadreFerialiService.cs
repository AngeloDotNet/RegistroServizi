using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.SquadreFeriali;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.SquadreFeriali;

namespace RegistroServizi.Models.Services.Application.SquadreFeriali
{
    public interface ISquadreFerialiService
    {
        Task<ListViewModel<SquadraFerialeViewModel>> GetSquadreFerialiAsync(SquadraFerialeListInputModel model);
        Task<SquadraFerialeEditInputModel> GetSquadraFerialeForEditingAsync(int id);
        Task<SquadraFerialeDetailViewModel> EditSquadraFerialeAsync(SquadraFerialeEditInputModel inputModel);
        Task<SquadraFerialeDetailViewModel> GetSquadraFerialeAsync(int id);
        Task<SquadraFerialeDetailViewModel> CreateSquadraFerialeAsync(SquadraFerialeCreateInputModel inputModel);
        Task DeleteSquadraFerialeAsync(SquadraFerialeDeleteInputModel inputModel);
    }
}