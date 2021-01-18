using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.Clienti;
using RegistroServizi.Models.Services.Application.Clienti;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ViewModels;
using RegistroServizi.Models.ViewModels.Clienti;

namespace RegistroServizi.Controllers
{
    public class ClientiController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly IClientiService clienti;
        public ClientiController(IApplicationPersister applicationPersister, IClientiService clienti)
        {
            this.applicationPersister= applicationPersister;
            this.clienti = clienti;
        }

        public async Task<IActionResult> Index(ClienteListInputModel input)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ListViewModel<ClienteViewModel> cliente = await clienti.GetClientiAsync(input);
            ClienteListViewModel viewModel = new ClienteListViewModel
            {
                Cliente = cliente,
                Input = input
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            var inputModel = new ClienteCreateInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClienteDetailViewModel cliente = await clienti.CreateClienteAsync(inputModel);
                    TempData["ConfirmationMessage"] = "Il cliente è stato creato con successo";
                    return RedirectToAction(nameof(Index));
                }
                catch (RagioneSocialeUnavailableException)
                {
                    ModelState.AddModelError(nameof(ClienteDetailViewModel.RagioneSociale), "La ragione sociale esiste già");
                }
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }

        public async Task<IActionResult> IsClienteAvailable(string ragionesociale, int id = 0)
        {
            bool result = await clienti.IsClienteAvailableAsync(ragionesociale, id);
            return Json(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ClienteDetailViewModel viewModel = await clienti.GetClienteAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ClienteEditInputModel inputModel = await clienti.GetClienteForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                ClienteDetailViewModel course = await clienti.EditClienteAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati aggiornati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View(inputModel);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(ClienteDeleteInputModel inputModel)
        {
            await clienti.DeleteClienteAsync(inputModel);
            TempData["ConfirmationMessage"] = "Il cliente è stata eliminato";
            return RedirectToAction(nameof(ClientiController.Detail), "Clienti", new { id = inputModel.Id });
        }
    }
}