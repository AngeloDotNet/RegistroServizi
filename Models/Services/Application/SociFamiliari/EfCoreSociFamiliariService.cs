using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.InputModels.SociFamiliari;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels.SociFamiliari;

namespace RegistroServizi.Models.Services.Application.SociFamiliari
{
    public class EfCoreSociFamiliariService : ISociFamiliariService
    {
        private readonly ILogger<EfCoreSociFamiliariService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreSociFamiliariService(ILogger<EfCoreSociFamiliariService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public Task<SocioFamiliareDetailViewModel> CreateSocioFamiliareAsync(SocioFamiliareCreateInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSocioFamiliareAsync(SocioFamiliareDeleteInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<SocioFamiliareDetailViewModel> EditSocioFamiliareAsync(SocioFamiliareEditInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<SocioFamiliareEditInputModel> GetSocioFamiliareForEditingAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsSocioFamiliareAvailableAsync(string familiare, int excludeId)
        {
            throw new System.NotImplementedException();
        }
    }
}