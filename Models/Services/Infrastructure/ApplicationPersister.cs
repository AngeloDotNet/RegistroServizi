using Microsoft.Extensions.Options;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public class ApplicationPersister : IApplicationPersister
    {
        private readonly IOptionsMonitor<ApplicationOptions> applicationOptions;

        public ApplicationPersister(IOptionsMonitor<ApplicationOptions> applicationOptions)
        {
            this.applicationOptions = applicationOptions;
        }  

        public string GetTitoloApp()
        {
            var option = applicationOptions.CurrentValue;
            string titolo = option.Titolo;

            return titolo;
        }   
    }
}