using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.InputModels.SquadreFestive;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Customizations.ModelBinders
{
    public class SquadraFestivoListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<SquadraFestivoOptions> squadraFestivoOptions;
        public SquadraFestivoListInputModelBinder(IOptionsMonitor<SquadraFestivoOptions> squadraFestivoOptions)
        {
            this.squadraFestivoOptions = squadraFestivoOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("Ascending").FirstValue, out bool ascending);

            SquadraFestivoOptions options = squadraFestivoOptions.CurrentValue;
            SquadraFestivoListInputModel inputModel = new(search, page, orderBy, ascending, options.PerPage, options.Order);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}