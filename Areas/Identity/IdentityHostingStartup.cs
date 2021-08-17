using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RegistroServizi.Areas.Identity.IdentityHostingStartup))]
namespace RegistroServizi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}