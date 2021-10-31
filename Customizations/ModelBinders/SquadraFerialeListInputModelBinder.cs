using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.SquadreFeriali;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class SquadraFerialeListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<SquadraFerialeOptions> squadraFerialeOptions;
        public SquadraFerialeListInputModelBinder(IOptionsMonitor<SquadraFerialeOptions> squadraFerialeOptions)
        {
            this.squadraFerialeOptions = squadraFerialeOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            SquadraFerialeOptions options = squadraFerialeOptions.CurrentValue;
            SquadraFerialeListInputModel inputModel = new(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}