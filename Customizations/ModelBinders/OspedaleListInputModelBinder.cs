using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.Ospedali;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class OspedaleListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<OspedaleOptions> ospedaleOptions;
        public OspedaleListInputModelBinder(IOptionsMonitor<OspedaleOptions> ospedaleOptions)
        {
            this.ospedaleOptions = ospedaleOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            OspedaleOptions options = ospedaleOptions.CurrentValue;
            OspedaleListInputModel inputModel = new(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}