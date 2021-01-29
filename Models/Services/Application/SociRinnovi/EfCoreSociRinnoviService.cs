using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.SociRinnovi;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels.SociRinnovi;

namespace RegistroServizi.Models.Services.Application.SociRinnovi
{
    public class EfCoreSociRinnoviService : ISociRinnoviService
    {
        private readonly ILogger<EfCoreSociRinnoviService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreSociRinnoviService(ILogger<EfCoreSociRinnoviService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<SocioRinnovoDetailViewModel> CreateSocioRinnovoAsync(SocioRinnovoCreateInputModel inputModel)
        {
            var socioRinnovo = new SocioRinnovo();

            socioRinnovo.ChangeSocioId(inputModel.SocioId);
            socioRinnovo.ChangeAnno(inputModel.Anno);
            socioRinnovo.ChangeQuota(inputModel.Quota);
            socioRinnovo.ChangeDataRinnovo(inputModel.DataRinnovo);

            dbContext.Add(socioRinnovo);
            await dbContext.SaveChangesAsync();

            return socioRinnovo.ToSocioRinnovoDetailViewModel();
        }

        public async Task DeleteSocioRinnovoAsync(SocioRinnovoDeleteInputModel inputModel)
        {
            SocioRinnovo socioRinnovo = await dbContext.SociRinnovi.FindAsync(inputModel.Id);

            if (socioRinnovo == null)
            {
                throw new SocioRinnovoNotFoundException(inputModel.Id);
            }

            dbContext.Remove(socioRinnovo);
            await dbContext.SaveChangesAsync();
        }

        public async Task<SocioRinnovoDetailViewModel> EditSocioRinnovoAsync(SocioRinnovoEditInputModel inputModel)
        {
            SocioRinnovo socioRinnovo = await dbContext.SociRinnovi.FindAsync(inputModel.Id);

            if (socioRinnovo == null)
            {
                logger.LogWarning("Rinnovo {id} del socio non trovato", inputModel.Id);
                throw new SocioRinnovoNotFoundException(inputModel.Id);
            }

            socioRinnovo.ChangeSocioId(inputModel.SocioId);
            socioRinnovo.ChangeAnno(inputModel.Anno);
            socioRinnovo.ChangeQuota(inputModel.Quota);
            socioRinnovo.ChangeDataRinnovo(inputModel.DataRinnovo);

            await dbContext.SaveChangesAsync();
            return socioRinnovo.ToSocioRinnovoDetailViewModel();
        }

        public async Task<SocioRinnovoEditInputModel> GetSocioRinnovoForEditingAsync(int id)
        {
            IQueryable<SocioRinnovoEditInputModel> queryLinq = dbContext.SociRinnovi
                .AsNoTracking()
                .Where(socioRinnovo => socioRinnovo.Id == id)
                .Select(socioRinnovo => socioRinnovo.ToSocioRinnovoEditInputModel());

            SocioRinnovoEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Rinnovo {id} del socio non trovato", id);
                throw new SocioRinnovoNotFoundException(id);
            }

            return viewModel;
        }
    }
}