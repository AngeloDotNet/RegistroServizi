using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.CostiServizi;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class CostoServizioListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<CostoServizioOptions> costoServizioOptions;
        public CostoServizioListInputModelBinder(IOptionsMonitor<CostoServizioOptions> costoServizioOptions)
        {
            this.costoServizioOptions = costoServizioOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            CostoServizioOptions options = costoServizioOptions.CurrentValue;
            var inputModel = new CostoServizioListInputModel(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}