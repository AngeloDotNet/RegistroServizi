using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.InputModels.CostiServizi;
using RegistroServizi.Models.Services.Application.CostiServizi;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.CostiServizi;

namespace RegistroServizi.Controllers
{
    public class CostiServiziController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly ICostiServiziService costiservizi;
        public CostiServiziController(IApplicationPersister applicationPersister, ICostiServiziService costiservizi)
        {
            this.applicationPersister= applicationPersister;
            this.costiservizi = costiservizi;
        }

        public async Task<IActionResult> Index(CostoServizioListInputModel input)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            
            ListViewModel<CostoServizioViewModel> costoservizio = await costiservizi.GetCostiServiziAsync(input);

            CostoServizioListViewModel viewModel = new CostoServizioListViewModel
            {
                CostoServizio = costoservizio,
                Input = input
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            CostoServizioDetailViewModel viewModel = await costiservizi.GetCostoServizioAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            CostoServizioEditInputModel inputModel = await costiservizi.GetCostoServizioForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CostoServizioEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                CostoServizioDetailViewModel costoservizio = await costiservizi.EditCostoServizioAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }
    }
}