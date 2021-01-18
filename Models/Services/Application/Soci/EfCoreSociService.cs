using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Extensions;
using RegistroServizi.Models.InputModels.Soci;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Soci;

namespace RegistroServizi.Models.Services.Application.Soci
{
    public class EfCoreSociService : ISociService
    {
        private readonly ILogger<EfCoreSociService> logger;
        private readonly RegistroServiziDbContext dbContext;
        public EfCoreSociService(ILogger<EfCoreSociService> logger, RegistroServiziDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<ListViewModel<SocioViewModel>> GetSociAsync(SocioListInputModel model)
        {
            IQueryable<Socio> baseQuery = dbContext.Soci;

            baseQuery = (model.OrderBy, model.Ascending) switch
            {
                ("Nominativo", true) => baseQuery.OrderBy(socio => socio.Nominativo),
                ("Nominativo", false) => baseQuery.OrderByDescending(socio => socio.Nominativo),
                ("Tessera", true) => baseQuery.OrderBy(socio => socio.Tessera),
                ("Tessera", false) => baseQuery.OrderByDescending(socio => socio.Tessera),
                ("Id", true) => baseQuery.OrderBy(socio => socio.Id),
                ("Id", false) => baseQuery.OrderByDescending(socio => socio.Id),
                _ => baseQuery
            };

            IQueryable<Socio> queryLinq = baseQuery
                .Where(socio => socio.Nominativo.Contains(model.Search))
                .AsNoTracking();

            List<SocioViewModel> socio = await queryLinq
                .Skip(model.Offset)
                .Take(model.Limit)
                .Select(socio => socio.ToSocioViewModel())
                .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<SocioViewModel> result = new ListViewModel<SocioViewModel>
            {
                Results = socio,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<SocioDetailViewModel> CreateSocioAsync(SocioCreateInputModel inputModel)
        {
            var socio = new Socio();

            socio.ChangeTessera(inputModel.Tessera);
            socio.ChangeNominativo(inputModel.Nominativo);
            socio.ChangeIndirizzo(inputModel.Indirizzo);
            socio.ChangeCap(inputModel.Cap);
            socio.ChangeComune(inputModel.Comune);
            socio.ChangeProvincia(inputModel.Provincia);
            socio.ChangeLuogoNascita(inputModel.LuogoNascita);
            socio.ChangeDataNascita(inputModel.DataNascita);
            socio.ChangeCodiceFiscale(inputModel.CodiceFiscale);
            socio.ChangeTelefono(inputModel.Telefono);
            socio.ChangeEmail(inputModel.Email);
            socio.ChangeDataTesseramento(inputModel.DataTesseramento);
            socio.ChangeTrattamentoDati(inputModel.TrattamentoDati);
            socio.ChangeProfessione(inputModel.Professione);
            socio.ChangeZona(inputModel.Zona);

            dbContext.Add(socio);
            await dbContext.SaveChangesAsync();

            return socio.ToSocioDetailSingleViewModel();
        }

        public async Task DeleteSocioAsync(SocioDeleteInputModel inputModel)
        {
            Socio socio = await dbContext.Soci.FindAsync(inputModel.Id);

            if (socio == null)
            {
                throw new SocioNotFoundException(inputModel.Id);
            }

            dbContext.Remove(socio);
            await dbContext.SaveChangesAsync();
        }

        public async Task<SocioDetailViewModel> GetSocioAsync(int id)
        {
            IQueryable<SocioDetailViewModel> queryLinq = dbContext.Soci
                .AsNoTracking()
                .Include(socio => socio.SociFamiliari)
                .Include(socio => socio.SociRinnovi)
                .Where(socio => socio.Id == id)
                .Select(socio => socio.ToSocioDetailViewModel());

            SocioDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Socio {id} non trovato", id);
                throw new SocioNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<string> GetLastRecordAsync()
        {
            int lastId = 0;
            int newId = 0;

            lastId = await dbContext.Soci
                            .OrderByDescending(x => x.Id)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

            logger.LogInformation("Ultimo ID Socio: {id}", lastId);

            newId = lastId + 1;
            logger.LogInformation("Nuovo ID Socio: {id}", newId);

            return newId.ToString();
        }

        public async Task<bool> IsSocioAvailableAsync(string nominativo, int id)
        {
            bool socioExists = await dbContext.Soci.AnyAsync(socio => EF.Functions.Like(socio.Nominativo, nominativo) && socio.Id != id);
            return !socioExists;
        }

        public async Task<bool> IsTesseraAvailableAsync(string tessera, int id)
        {
            bool tesseraExists = await dbContext.Soci.AnyAsync(socio => EF.Functions.Like(socio.Tessera, tessera) && socio.Id != id);
            return !tesseraExists;
        }

        public async Task<SocioEditInputModel> GetSocioForEditingAsync(int id)
        {
            IQueryable<SocioEditInputModel> queryLinq = dbContext.Soci
                .AsNoTracking()
                .Where(socio => socio.Id == id)
                .Select(socio => socio.ToSocioEditInputModel());

            SocioEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Socio {id} non trovato", id);
                throw new SocioNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<SocioDetailViewModel> EditSocioAsync(SocioEditInputModel inputModel)
        {
            Socio socio = await dbContext.Soci.FindAsync(inputModel.Id);

            if (socio == null)
            {
                logger.LogWarning("Socio {id} non trovato", inputModel.Id);
                throw new ClienteNotFoundException(inputModel.Id);
            }

            socio.ChangeTessera(inputModel.Tessera);
            socio.ChangeNominativo(inputModel.Nominativo);
            socio.ChangeIndirizzo(inputModel.Indirizzo);
            socio.ChangeCap(inputModel.Cap);
            socio.ChangeComune(inputModel.Comune);
            socio.ChangeProvincia(inputModel.Provincia);
            socio.ChangeLuogoNascita(inputModel.LuogoNascita);
            socio.ChangeDataNascita(inputModel.DataNascita);
            socio.ChangeCodiceFiscale(inputModel.CodiceFiscale);
            socio.ChangeTelefono(inputModel.Telefono);
            socio.ChangeEmail(inputModel.Email);
            socio.ChangeDataTesseramento(inputModel.DataTesseramento);
            socio.ChangeTrattamentoDati(inputModel.TrattamentoDati);
            socio.ChangeProfessione(inputModel.Professione);
            socio.ChangeZona(inputModel.Zona);

            await dbContext.SaveChangesAsync();
            return socio.ToSocioDetailSingleViewModel();
        }
    }
}