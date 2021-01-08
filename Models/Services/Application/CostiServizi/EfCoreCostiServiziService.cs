using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.CostiServizi;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.CostiServizi;

namespace RegistroServizi.Models.Services.Application.CostiServizi
{
    public class EfCoreCostiServiziService : ICostiServiziService
    {
        private readonly ILogger<EfCoreCostiServiziService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreCostiServiziService(ILogger<EfCoreCostiServiziService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<ListViewModel<CostoServizioViewModel>> GetCostiServiziAsync(CostoServizioListInputModel model)
        {
            IQueryable<Costo> baseQuery = dbContext.Costi;

            baseQuery = (model.OrderBy, model.Ascending) switch
            {
                ("TipoServizio", true) => baseQuery.OrderBy(costoservizio => costoservizio.TipoServizio),
                ("TipoServizio", false) => baseQuery.OrderByDescending(costoservizio => costoservizio.TipoServizio),
                ("CostoFisso", true) => baseQuery.OrderBy(costoservizio => costoservizio.CostoFisso),
                ("CostoFisso", false) => baseQuery.OrderByDescending(costoservizio => costoservizio.CostoFisso),
                ("Id", true) => baseQuery.OrderBy(costoservizio => costoservizio.Id),
                ("Id", false) => baseQuery.OrderByDescending(costoservizio => costoservizio.Id),
                _ => baseQuery
            };

            IQueryable<Costo> queryLinq = baseQuery
                .AsNoTracking();

            List<CostoServizioViewModel> costo = await queryLinq
                .Skip(model.Offset)
                .Take(model.Limit)
                .Select(costo => costo.ToCostoServizioViewModel())
                .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<CostoServizioViewModel> result = new ListViewModel<CostoServizioViewModel>
            {
                Results = costo,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<CostoServizioEditInputModel> GetCostoServizioForEditingAsync(int id)
        {
            IQueryable<CostoServizioEditInputModel> queryLinq = dbContext.Costi
                .AsNoTracking()
                .Where(costo => costo.Id == id)
                .Select(costo => costo.ToCostoServizioEditInputModel());

            CostoServizioEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Costo del servizio {id} non trovato", id);
                throw new CostoServizioNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<CostoServizioDetailViewModel> EditCostoServizioAsync(CostoServizioEditInputModel inputModel)
        {
            Costo costo = await dbContext.Costi.FindAsync(inputModel.Id);

            if (costo == null)
            {
                logger.LogWarning("Costo del servizio {id} non trovato", inputModel.Id);
                throw new CostoServizioNotFoundException(inputModel.Id);
            }

            costo.ChangeTipoServizio(inputModel.TipoServizio);
            costo.ChangeCostoFisso(inputModel.CostoFisso);
            costo.ChangeCostoKm(inputModel.CostoKm);
            costo.ChangeSecondoTrasportato(inputModel.SecondoTrasportato);
            costo.ChangeFermoMacchina(inputModel.FermoMacchina);
            costo.ChangeAccompagnatore(inputModel.Accompagnatore);
            costo.ChangeScontoSoci(inputModel.ScontoSoci);

            await dbContext.SaveChangesAsync();
            return costo.ToCostoServizioDetailViewModel();
        }

        public async Task<CostoServizioDetailViewModel> GetCostoServizioAsync(int id)
        {
            IQueryable<CostoServizioDetailViewModel> queryLinq = dbContext.Costi
                .AsNoTracking()
                .Where(costo => costo.Id == id)
                .Select(costo => costo.ToCostoServizioDetailViewModel());

            CostoServizioDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Costo del servizio {id} non trovato", id);
                throw new CostoServizioNotFoundException(id);
            }

            return viewModel;
        }
    }
}