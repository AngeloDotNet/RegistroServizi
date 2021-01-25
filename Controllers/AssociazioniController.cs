using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.InputModels.Associazioni;
using RegistroServizi.Models.Services.Application.Associazioni;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Associazioni;

namespace RegistroServizi.Controllers
{
    public class AssociazioniController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly IAssociazioniService associazioni;
        public AssociazioniController(IApplicationPersister applicationPersister, IAssociazioniService associazioni)
        {
            this.applicationPersister= applicationPersister;
            this.associazioni = associazioni;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ListViewModel<AssociazioneViewModel> associazione = await associazioni.GetAssociazioniAsync();
            AssociazioneListViewModel viewModel = new()
            {
                Associazione = associazione
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            AssociazioneDetailViewModel viewModel = await associazioni.GetAssociazioneAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            AssociazioneEditInputModel inputModel = await associazioni.GetAssociazioneForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssociazioneEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                AssociazioneDetailViewModel associazione = await associazioni.EditAssociazioneAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }
    }
}