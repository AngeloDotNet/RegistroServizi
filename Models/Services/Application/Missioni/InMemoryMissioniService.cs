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
        private static ConcurrentBag<MissioneViewModel> missioni = new ConcurrentBag<MissioneViewModel>();

        public InMemoryMissioniService(IPessimisticLock pessimisticLock)
        {
            this.pessimisticLock = pessimisticLock;
        }
        
        public Task<MissioneViewModel> CreateMissioneAsync(MissioneCreateInputModel inputModel)
        {
            var data = DateTime.Today;
            var id = missioni.Count == 0 ? 1 : missioni.Max(missione => missione.Id) + 1; // TODO: In questo esempio l'id è un progressivo
            var missione = new MissioneViewModel
            {
                Id = id,
                Data = data,
                Tipologia = inputModel.Tipologia,
                Titolo = $"Missione {id} del {data:dd/MM/yyyy}"
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

        public Task<bool> CanEditMissioneAsync(int idMissione)
        {
            return pessimisticLock.CanLockMissioneAsync(idMissione);
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
            await pessimisticLock.ReleaseLockForMissioneAsync(inputModel.Id);
            return missione;
        }

        private async Task EnsureCanLockMissione(int idMissione)
        {
            if (!await pessimisticLock.RefreshLockForMissioneAsync(idMissione))
            {
                throw new PessimisticLockAcquireException();
            }
        }
    }
}