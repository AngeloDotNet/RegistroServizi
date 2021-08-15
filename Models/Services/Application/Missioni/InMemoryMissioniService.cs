using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.Missioni;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels.Missioni;

namespace RegistroServizi.Models.Services.Application.Missioni
{
    public class InMemoryMissioniService : IMissioniService
    {
        private readonly IPessimisticLock pessimisticLock;
        private ConcurrentBag<MissioneViewModel> missioni = new ConcurrentBag<MissioneViewModel>();

        public InMemoryMissioniService(IPessimisticLock pessimisticLock)
        {
            this.pessimisticLock = pessimisticLock;
        }
        
        public Task<MissioneViewModel> CreateMissioneAsync(MissioneCreateInputModel inputModel)
        {
            var missione = new MissioneViewModel
            {
                Id = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds()), // TODO: In questo esempio l'id viene generato in base alla data/ora attuale
                Data = DateTime.UtcNow,
                Tipologia = inputModel.Tipologia,
                Titolo = string.Empty
            };

            missioni.Add(missione);
            return Task.FromResult(missione);
        }

        public async Task<MissioneEditInputModel> GetMissioneForEditingAsync(int idMissione)
        {
            await EnsureCanLockMissione(idMissione);

            var missione = missioni.Single(m => m.Id == idMissione);
            return new MissioneEditInputModel
            {
                Id = missione.Id,
                Data = missione.Data,
                Tipologia = missione.Tipologia,
                Titolo = missione.Titolo
            };
        }

        public Task<IList<MissioneViewModel>> GetMissioniAsync()
        {
            return Task.FromResult<IList<MissioneViewModel>>(missioni.ToList());
        }

        public Task RefreshEditingMissioneAsync(int idMissione)
        {
            return EnsureCanLockMissione(idMissione);
        }

        public async Task<MissioneViewModel> EditMissionAsync(MissioneEditInputModel inputModel)
        {
            await EnsureCanLockMissione(inputModel.Id);
            var missione = missioni.Single(viewModel => viewModel.Id == inputModel.Id);
            missione.Data = inputModel.Data;
            missione.Titolo = inputModel.Titolo;
            missione.Tipologia = inputModel.Tipologia;
            await pessimisticLock.ReleaseLockForMissione(inputModel.Id);
            return missione;
        }

        private async Task EnsureCanLockMissione(int idMissione)
        {
            if (!await pessimisticLock.TryAcquireLockForMissione(idMissione))
            {
                throw new PessimisticLockAcquireException();
            }
        }
    }
}