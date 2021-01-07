using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.Associazioni;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Associazioni;

namespace RegistroServizi.Models.Services.Application.Associazioni
{
    public interface IAssociazioniService
    {
        Task<ListViewModel<AssociazioneViewModel>> GetAssociazioniAsync();
        Task<AssociazioneEditInputModel> GetAssociazioneForEditingAsync(int id);
        Task<AssociazioneDetailViewModel> EditAssociazioneAsync(AssociazioneEditInputModel inputModel);
        Task<AssociazioneDetailViewModel> GetAssociazioneAsync(int id);
    }
}