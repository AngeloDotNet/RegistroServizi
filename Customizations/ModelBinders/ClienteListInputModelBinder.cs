using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.Clienti;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class ClienteListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<ClienteOptions> clienteOptions;
        public ClienteListInputModelBinder(IOptionsMonitor<ClienteOptions> clienteOptions)
        {
            this.clienteOptions = clienteOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            ClienteOptions options = clienteOptions.CurrentValue;
            var inputModel = new ClienteListInputModel(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}