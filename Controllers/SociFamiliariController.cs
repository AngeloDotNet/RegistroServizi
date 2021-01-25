using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.SociFamiliari;
using RegistroServizi.Models.Services.Application.SociFamiliari;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels.SociFamiliari;

namespace RegistroServizi.Controllers
{
    public class SociFamiliariController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly ISociFamiliariService socifamiliari;
        public SociFamiliariController(IApplicationPersister applicationPersister, ISociFamiliariService socifamiliari)
        {
            this.applicationPersister= applicationPersister;
            this.socifamiliari = socifamiliari;
        }

        public async Task<IActionResult> IsSocioFamiliareAvailable(string familiare, int id = 0)
        {
            bool result = await socifamiliari.IsSocioFamiliareAvailableAsync(familiare, id);
            return Json(result);
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioFamiliareCreateInputModel inputModel = new();
            inputModel.SocioId = id;
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SocioFamiliareCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SocioFamiliareDetailViewModel socio = await socifamiliari.CreateSocioFamiliareAsync(inputModel);
                    TempData["ConfirmationMessage"] = "Il socio familiare è stato creato con successo";
                    return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
                }
                catch (SocioFamiliareUnavailableException)
                {
                    ModelState.AddModelError(nameof(SocioFamiliareDetailViewModel.Familiare), "Il nominativo del socio familiare esiste già");
                }
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            SocioFamiliareEditInputModel inputModel = await socifamiliari.GetSocioFamiliareForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SocioFamiliareEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                SocioFamiliareDetailViewModel socio = await socifamiliari.EditSocioFamiliareAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SocioFamiliareDeleteInputModel inputModel)
        {
            await socifamiliari.DeleteSocioFamiliareAsync(inputModel);
            TempData["ConfirmationMessage"] = "Il socio familiare è stato eliminato";
            return RedirectToAction(nameof(SociController.Detail), "Soci", new { id = inputModel.SocioId });
        }
    }
}