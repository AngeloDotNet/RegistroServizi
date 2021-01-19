using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
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

        public async Task<SocioFamiliareDetailViewModel> CreateSocioFamiliareAsync(SocioFamiliareCreateInputModel inputModel)
        {
            var socioFamiliare = new SocioFamiliare();

            socioFamiliare.ChangeSocioId(inputModel.SocioId);
            socioFamiliare.ChangeFamiliare(inputModel.Familiare);

            dbContext.Add(socioFamiliare);
            await dbContext.SaveChangesAsync();

            return socioFamiliare.ToSocioFamiliareDetailViewModel();
        }

        public async Task DeleteSocioFamiliareAsync(SocioFamiliareDeleteInputModel inputModel)
        {
            SocioFamiliare socioFamiliare = await dbContext.SociFamiliari.FindAsync(inputModel.Id);

            if (socioFamiliare == null)
            {
                throw new SocioFamiliareNotFoundException(inputModel.Id);
            }

            dbContext.Remove(socioFamiliare);
            await dbContext.SaveChangesAsync();
        }

        public async Task<SocioFamiliareDetailViewModel> EditSocioFamiliareAsync(SocioFamiliareEditInputModel inputModel)
        {
            SocioFamiliare socioFamiliare = await dbContext.SociFamiliari.FindAsync(inputModel.Id);

            if (socioFamiliare == null)
            {
                logger.LogWarning("Socio familiare {id} non trovato", inputModel.Id);
                throw new SocioFamiliareNotFoundException(inputModel.Id);
            }

            socioFamiliare.ChangeSocioId(inputModel.SocioId);
            socioFamiliare.ChangeFamiliare(inputModel.Familiare);

            await dbContext.SaveChangesAsync();
            return socioFamiliare.ToSocioFamiliareDetailViewModel();
        }

        public async Task<SocioFamiliareEditInputModel> GetSocioFamiliareForEditingAsync(int id)
        {
            IQueryable<SocioFamiliareEditInputModel> queryLinq = dbContext.SociFamiliari
                .AsNoTracking()
                .Where(socioFamiliare => socioFamiliare.Id == id)
                .Select(socioFamiliare => socioFamiliare.ToSocioFamiliareEditInputModel());

            SocioFamiliareEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Socio familiare {id} non trovato", id);
                throw new SocioFamiliareNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<bool> IsSocioFamiliareAvailableAsync(string familiare, int id)
        {
            bool socioFamiliareExists = await dbContext.SociFamiliari.AnyAsync(socio => EF.Functions.Like(socio.Familiare, familiare) && socio.Id != id);
            return !socioFamiliareExists;
        }
    }
}