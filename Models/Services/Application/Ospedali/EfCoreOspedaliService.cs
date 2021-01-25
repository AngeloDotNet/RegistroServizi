using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Models.Services.Application.Ospedali
{
    public class EfCoreOspedaliService : IOspedaliService
    {
        private readonly ILogger<EfCoreOspedaliService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreOspedaliService(ILogger<EfCoreOspedaliService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }
    }
}