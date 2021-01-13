using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Enums;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.Clienti;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Clienti;

namespace RegistroServizi.Models.Services.Application.Clienti
{
    public class EfCoreClientiService : IClientiService
    {
        private readonly ILogger<EfCoreClientiService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreClientiService(ILogger<EfCoreClientiService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<ListViewModel<ClienteViewModel>> GetClientiAsync(ClienteListInputModel model)
        {
            IQueryable<Cliente> baseQuery = dbContext.Clienti;

            baseQuery = (model.OrderBy, model.Ascending) switch
            {
                ("RagioneSociale", true) => baseQuery.OrderBy(Cliente => Cliente.RagioneSociale),
                ("RagioneSociale", false) => baseQuery.OrderByDescending(Cliente => Cliente.RagioneSociale),
                ("Id", true) => baseQuery.OrderBy(costoservizio => costoservizio.Id),
                ("Id", false) => baseQuery.OrderByDescending(costoservizio => costoservizio.Id),
                _ => baseQuery
            };

            IQueryable<Cliente> queryLinq = baseQuery
                .AsNoTracking();

            List<ClienteViewModel> cliente = await queryLinq
                .Skip(model.Offset)
                .Take(model.Limit)
                .Select(cliente => cliente.ToClienteViewModel())
                .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<ClienteViewModel> result = new ListViewModel<ClienteViewModel>
            {
                Results = cliente,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> IsClienteAvailableAsync(string ragionesociale, int id)
        {
            bool clienteExists = await dbContext.Clienti.AnyAsync(cliente => EF.Functions.Like(cliente.RagioneSociale, ragionesociale) && cliente.Id != id);
            return !clienteExists;
        }

        public async Task<ClienteDetailViewModel> CreateClienteAsync(ClienteCreateInputModel inputModel)
        {
            var cliente = new Cliente();
            
            cliente.ChangeRagioneSociale(inputModel.RagioneSociale);
            cliente.ChangeIndirizzo(inputModel.Indirizzo);
            cliente.ChangeComune(inputModel.Comune);
            cliente.ChangeCap(inputModel.Cap);
            cliente.ChangeProvincia(inputModel.Provincia);
            cliente.ChangeTelefono(inputModel.Telefono);
            cliente.ChangeFax(inputModel.Fax);
            cliente.ChangeCodiceFiscale(inputModel.CodiceFiscale);
            cliente.ChangePartitaIva(inputModel.PartitaIva);
            cliente.ChangeSpese(inputModel.Spese);

            dbContext.Add(cliente);
            await dbContext.SaveChangesAsync();

            return cliente.ToClienteDetailViewModel();
        }
        
        public async Task<ClienteDetailViewModel> EditClienteAsync(ClienteEditInputModel inputModel)
        {
            Cliente cliente = await dbContext.Clienti.FindAsync(inputModel.Id);

            if (cliente == null)
            {
                logger.LogWarning("Cliente {id} non trovato", inputModel.Id);
                throw new ClienteNotFoundException(inputModel.Id);
            }

            cliente.ChangeRagioneSociale(inputModel.RagioneSociale);
            cliente.ChangeIndirizzo(inputModel.Indirizzo);
            cliente.ChangeComune(inputModel.Comune);
            cliente.ChangeCap(inputModel.Cap);
            cliente.ChangeProvincia(inputModel.Provincia);
            cliente.ChangeTelefono(inputModel.Telefono);
            cliente.ChangeFax(inputModel.Fax);
            cliente.ChangeCodiceFiscale(inputModel.CodiceFiscale);
            cliente.ChangePartitaIva(inputModel.PartitaIva);
            cliente.ChangeSpese(inputModel.Spese);

            await dbContext.SaveChangesAsync();
            return cliente.ToClienteDetailViewModel();
        }

        public async Task<ClienteDetailViewModel> GetClienteAsync(int id)
        {
            IQueryable<ClienteDetailViewModel> queryLinq = dbContext.Clienti
                .AsNoTracking()
                .Where(cliente => cliente.Id == id)
                .Select(cliente => cliente.ToClienteDetailViewModel());

            ClienteDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Cliente {id} non trovato", id);
                throw new ClienteNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<ClienteEditInputModel> GetClienteForEditingAsync(int id)
        {
            IQueryable<ClienteEditInputModel> queryLinq = dbContext.Clienti
                .AsNoTracking()
                .Where(cliente => cliente.Id == id)
                .Select(cliente => cliente.ToClienteEditInputModel());

            ClienteEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Cliente {id} non trovato", id);
                throw new ClienteNotFoundException(id);
            }

            return viewModel;
        }

        public async Task DeleteClienteAsync(ClienteDeleteInputModel inputModel)
        {
            Cliente cliente = await dbContext.Clienti.FindAsync(inputModel.Id);

            if (cliente == null)
            {
                throw new ClienteNotFoundException(inputModel.Id);
            }

            dbContext.Remove(cliente);
            await dbContext.SaveChangesAsync();
        }
    }
}