using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.Soci;
using RegistroServizi.Models.Services.Application.Soci;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Soci;

namespace RegistroServizi.Controllers
{
    public class SociController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly ISociService soci;
        public SociController(IApplicationPersister applicationPersister, ISociService soci)
        {
            this.applicationPersister= applicationPersister;
            this.soci = soci;
        }

        public async Task<IActionResult> Index(SocioListInputModel input)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ListViewModel<SocioViewModel> socio = await soci.GetSociAsync(input);
            SocioListViewModel viewModel = new()
            {
                Socio = socio,
                Input = input
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IsSocioAvailable(string nominativo, int id = 0)
        {
            bool result = await soci.IsSocioAvailableAsync(nominativo, id);
            return Json(result);
        }

        public async Task<IActionResult> IsTesseraAvailable(string tessera, int id = 0)
        {
            bool result = await soci.IsTesseraAvailableAsync(tessera, id);
            return Json(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioCreateInputModel inputModel = new();
            inputModel.Tessera = await soci.GetNextIdAsync();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SocioCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SocioDetailViewModel socio = await soci.CreateSocioAsync(inputModel);
                    TempData["ConfirmationMessage"] = "Il socio è stato creato con successo";
                    return RedirectToAction(nameof(Detail), new { id = socio.Id });
                }
                catch (SocioUnavailableException)
                {
                    ModelState.AddModelError(nameof(SocioDetailViewModel.Nominativo), "Il nominativo del socio esiste già");
                }
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();

            bool StatusSocio = await soci.IsSocioRegolareAsync(id);
            if (StatusSocio == true)
            {
                ViewData["StatoSocio"] = "In regola";
            }
            else
            {
                ViewData["StatoSocio"] = "Non in regola";
            }

            SocioDetailViewModel viewModel = await soci.GetSocioAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioEditInputModel inputModel = await soci.GetSocioForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SocioEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                SocioDetailViewModel socio = await soci.EditSocioAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(SocioDeleteInputModel inputModel)
        {
            await soci.DeleteSocioAsync(inputModel);
            TempData["ConfirmationMessage"] = "Il socio è stato eliminato";
            return RedirectToAction(nameof(SociController.Index));
        }
    }
}