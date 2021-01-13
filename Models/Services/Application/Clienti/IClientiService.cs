using System.Threading.Tasks;
using RegistroServizi.Models.InputModels.Clienti;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Clienti;

namespace RegistroServizi.Models.Services.Application.Clienti
{
    public interface IClientiService
    {
        Task<ListViewModel<ClienteViewModel>> GetClientiAsync(ClienteListInputModel model);
        Task<ClienteEditInputModel> GetClienteForEditingAsync(int id);
        Task<ClienteDetailViewModel> EditClienteAsync(ClienteEditInputModel inputModel);
        Task<ClienteDetailViewModel> GetClienteAsync(int id);
        Task<ClienteDetailViewModel> CreateClienteAsync(ClienteCreateInputModel inputModel);
        Task<bool> IsClienteAvailableAsync(string ragionesociale, int excludeId);
        Task DeleteClienteAsync(ClienteDeleteInputModel inputModel);
    }
}