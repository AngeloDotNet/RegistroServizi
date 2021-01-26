using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.InputModels.SociRinnovi;
using RegistroServizi.Models.Services.Application.SociRinnovi;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels.SociRinnovi;

namespace RegistroServizi.Controllers
{
    public class SociRinnoviController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly ISociRinnoviService sociRinnovi;
        public SociRinnoviController(IApplicationPersister applicationPersister, ISociRinnoviService sociRinnovi)
        {
            this.applicationPersister= applicationPersister;
            this.sociRinnovi = sociRinnovi;
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioRinnovoCreateInputModel inputModel = new();
            inputModel.SocioId = id;
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SocioRinnovoCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                SocioRinnovoDetailViewModel socio = await sociRinnovi.CreateSocioRinnovoAsync(inputModel);
                    TempData["ConfirmationMessage"] = "Il rinnovo del socio è stato creato con successo";
                    return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioRinnovoEditInputModel inputModel = await sociRinnovi.GetSocioRinnovoForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SocioRinnovoEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                SocioRinnovoDetailViewModel socio = await sociRinnovi.EditSocioRinnovoAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SocioRinnovoDeleteInputModel inputModel)
        {
            await sociRinnovi.DeleteSocioRinnovoAsync(inputModel);
            TempData["ConfirmationMessage"] = "Il rinnovo del socio è stato eliminato";
            return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
        }
    }
}