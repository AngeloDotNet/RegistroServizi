using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.Ospedali;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Ospedali;

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

        public async Task<ListViewModel<OspedaleViewModel>> GetOspedaliAsync(OspedaleListInputModel model)
        {
            IQueryable<Ospedale> baseQuery = dbContext.Ospedali;

            baseQuery = (model.OrderBy, model.Ascending) switch
            {
                ("Ospedale", true) => baseQuery.OrderBy(ospedale => ospedale.Clinica),
                ("Ospedale", false) => baseQuery.OrderByDescending(ospedale => ospedale.Clinica),
                ("Id", true) => baseQuery.OrderBy(ospedale => ospedale.Id),
                ("Id", false) => baseQuery.OrderByDescending(ospedale => ospedale.Id),
                _ => baseQuery
            };

            IQueryable<Ospedale> queryLinq = baseQuery
                .Where(ospedale => ospedale.Clinica.Contains(model.Search))
                .AsNoTracking();

            List<OspedaleViewModel> ospedale = await queryLinq
                .Skip(model.Offset)
                .Take(model.Limit)
                .Select(ospedale => ospedale.ToOspedaleViewModel())
                .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<OspedaleViewModel> result = new ListViewModel<OspedaleViewModel>
            {
                Results = ospedale,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> IsOspedaleAvailableAsync(string clinica, int id)
        {
            bool ospedaleExists = await dbContext.Ospedali.AnyAsync(ospedale => EF.Functions.Like(ospedale.Clinica, clinica) && ospedale.Id != id);
            return !ospedaleExists;
        }

        public async Task<OspedaleDetailViewModel> CreateOspedaleAsync(OspedaleCreateInputModel inputModel)
        {
            var ospedale = new Ospedale();
            
            ospedale.ChangeClinica(inputModel.Clinica);
            ospedale.ChangeComune(inputModel.Comune);
            ospedale.ChangeLatitudine(inputModel.Latitudine);
            ospedale.ChangeLongitudine(inputModel.Longitudine);

            dbContext.Add(ospedale);
            await dbContext.SaveChangesAsync();

            return ospedale.ToOspedaleDetailViewModel();
        }
        
        public async Task<OspedaleDetailViewModel> EditOspedaleAsync(OspedaleEditInputModel inputModel)
        {
            Ospedale ospedale = await dbContext.Ospedali.FindAsync(inputModel.Id);

            if (ospedale == null)
            {
                logger.LogWarning("Ospedale {id} non trovato", inputModel.Id);
                throw new OspedaleNotFoundException(inputModel.Id);
            }

            ospedale.ChangeClinica(inputModel.Clinica);
            ospedale.ChangeComune(inputModel.Comune);
            ospedale.ChangeLatitudine(inputModel.Latitudine);
            ospedale.ChangeLongitudine(inputModel.Longitudine);

            await dbContext.SaveChangesAsync();
            return ospedale.ToOspedaleDetailViewModel();
        }

        public async Task<OspedaleDetailViewModel> GetOspedaleAsync(int id)
        {
            IQueryable<OspedaleDetailViewModel> queryLinq = dbContext.Ospedali
                .AsNoTracking()
                .Where(ospedale => ospedale.Id == id)
                .Select(ospedale => ospedale.ToOspedaleDetailViewModel());

            OspedaleDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Ospedale {id} non trovato", id);
                throw new OspedaleNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<OspedaleEditInputModel> GetOspedaleForEditingAsync(int id)
        {
            IQueryable<OspedaleEditInputModel> queryLinq = dbContext.Ospedali
                .AsNoTracking()
                .Where(ospedale => ospedale.Id == id)
                .Select(ospedale => ospedale.ToOspedaleEditInputModel());

            OspedaleEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Ospedale {id} non trovato", id);
                throw new OspedaleNotFoundException(id);
            }

            return viewModel;
        }

        public async Task DeleteOspedaleAsync(OspedaleDeleteInputModel inputModel)
        {
            Ospedale ospedale = await dbContext.Ospedali.FindAsync(inputModel.Id);

            if (ospedale == null)
            {
                throw new OspedaleNotFoundException(inputModel.Id);
            }

            dbContext.Remove(ospedale);
            await dbContext.SaveChangesAsync();
        }
    }
}