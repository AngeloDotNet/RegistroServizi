using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.Soci;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class SocioListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<SocioOptions> clienteOptions;
        public SocioListInputModelBinder(IOptionsMonitor<SocioOptions> clienteOptions)
        {
            this.clienteOptions = clienteOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            SocioOptions options = clienteOptions.CurrentValue;
            var inputModel = new SocioListInputModel(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}