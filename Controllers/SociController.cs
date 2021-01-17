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

            SocioListViewModel viewModel = new SocioListViewModel
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

        public IActionResult Create()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            var inputModel = new SocioCreateInputModel();
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
            TempData["ConfirmationMessage"] = "Il socio è stata eliminato";
            return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.Id });
        }
    }
}