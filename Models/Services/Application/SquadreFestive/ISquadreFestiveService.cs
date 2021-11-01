using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.SquadreFestive;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.SquadreFestive;

namespace RegistroServizi.Models.Services.Application.SquadreFestive
{
    public interface ISquadreFestiveService
    {
        Task<ListViewModel<SquadraFestivoViewModel>> GetSquadreFerialiAsync(SquadraFestivoListInputModel model);
        Task<SquadraFestivoEditInputModel> GetSquadraFerialeForEditingAsync(int id);
        Task<SquadraFestivoDetailViewModel> EditSquadraFerialeAsync(SquadraFestivoEditInputModel inputModel);
        Task<SquadraFestivoDetailViewModel> GetSquadraFerialeAsync(int id);
        Task<SquadraFestivoDetailViewModel> CreateSquadraFerialeAsync(SquadraFestivoCreateInputModel inputModel);
        Task DeleteSquadraFerialeAsync(SquadraFestivoDeleteInputModel inputModel);
    }
}