using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.Ospedali;
using RegistroServizi.Models.Services.Application.Ospedali;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Ospedali;

namespace RegistroServizi.Controllers
{
    public class OspedaliController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly IOspedaliService ospedali;
        public OspedaliController(IApplicationPersister applicationPersister, IOspedaliService ospedali)
        {
            this.applicationPersister= applicationPersister;
            this.ospedali = ospedali;
        }

        public async Task<IActionResult> Index(OspedaleListInputModel input)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ListViewModel<OspedaleViewModel> ospedale = await ospedali.GetOspedaliAsync(input);
            OspedaleListViewModel viewModel = new()
            {
                Ospedale = ospedale,
                Input = input
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IsOspedaleAvailable(string clinica, int id = 0)
        {
            bool result = await ospedali.IsOspedaleAvailableAsync(clinica, id);
            return Json(result);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            OspedaleCreateInputModel inputModel = new();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OspedaleCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OspedaleDetailViewModel ospedale = await ospedali.CreateOspedaleAsync(inputModel);
                    TempData["ConfirmationMessage"] = "L'ospedale è stato creato con successo";
                    return RedirectToAction(nameof(Detail), new { id = ospedale.Id });
                }
                catch (OspedaleUnavailableException)
                {
                    ModelState.AddModelError(nameof(OspedaleDetailViewModel.Clinica), "L'ospedale esiste già");
                }
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            OspedaleDetailViewModel viewModel = await ospedali.GetOspedaleAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            OspedaleEditInputModel inputModel = await ospedali.GetOspedaleForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OspedaleEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                OspedaleDetailViewModel ospedale = await ospedali.EditOspedaleAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(OspedaleDeleteInputModel inputModel)
        {
            await ospedali.DeleteOspedaleAsync(inputModel);
            TempData["ConfirmationMessage"] = "L'ospedale è stato eliminato";
            return RedirectToAction(nameof(OspedaliController.Index));
        }
    }
}