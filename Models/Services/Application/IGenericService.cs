using System.Threading.Tasks;

namespace RegistroServizi.Models.Services.Application
{
    public interface IGenericService
    {
        Task SendQuestionAsync(string appEmail, string question);
    }
}