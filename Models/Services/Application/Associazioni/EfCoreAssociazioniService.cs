using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.Associazioni;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Associazioni;

namespace RegistroServizi.Models.Services.Application.Associazioni
{
    public class EfCoreAssociazioniService : IAssociazioniService
    {
        private readonly ILogger<EfCoreAssociazioniService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreAssociazioniService(ILogger<EfCoreAssociazioniService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<ListViewModel<AssociazioneViewModel>> GetAssociazioniAsync()
        {
            IQueryable<Associazione> baseQuery = dbContext.Associazioni;

            IQueryable<Associazione> queryLinq = baseQuery
                .AsNoTracking();

            List<AssociazioneViewModel> associazioni = await queryLinq
                .Select(associazione => associazione.ToAssociazioneViewModel())
                .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<AssociazioneViewModel> result = new ListViewModel<AssociazioneViewModel>
            {
                Results = associazioni
            };

            return result;
        }

        public async Task<AssociazioneEditInputModel> GetAssociazioneForEditingAsync(int id)
        {
            IQueryable<AssociazioneEditInputModel> queryLinq = dbContext.Associazioni
                .AsNoTracking()
                .Where(associazione => associazione.Id == id)
                .Select(associazione => AssociazioneEditInputModel.FromEntity(associazione));

            AssociazioneEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Associazione {id} non trovata", id);
                throw new AssociazioneNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<AssociazioneDetailViewModel> EditAssociazioneAsync(AssociazioneEditInputModel inputModel)
        {
            Associazione associazione = await dbContext.Associazioni.FindAsync(inputModel.Id);

            if (associazione == null)
            {
                logger.LogWarning("Associazione {id} non trovata", inputModel.Id);
                throw new AssociazioneNotFoundException(inputModel.Id);
            }

            associazione.ChangeDenominazione(inputModel.Denominazione);
            associazione.ChangeSigla(inputModel.Sigla);
            associazione.ChangeIndirizzo(inputModel.Indirizzo);
            associazione.ChangeCap(inputModel.Cap);
            associazione.ChangeComune(inputModel.Comune);
            associazione.ChangeProvincia(inputModel.Provincia);

            await dbContext.SaveChangesAsync();

            return associazione.ToAssociazioneDetailViewModel();
        }

        public async Task<AssociazioneDetailViewModel> GetAssociazioneAsync(int id)
        {
            IQueryable<AssociazioneDetailViewModel> queryLinq = dbContext.Associazioni
                .AsNoTracking()
                .Where(associazione => associazione.Id == id)
                .Select(associazione => associazione.ToAssociazioneDetailViewModel());

            AssociazioneDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Associazione {id} non trovata", id);
                throw new AssociazioneNotFoundException(id);
            }

            return viewModel;
        }
    }
}