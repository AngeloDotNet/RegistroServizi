using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.InputModels.Missioni;
using RegistroServizi.Models.Services.Application.Missioni;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Controllers
{
    public class MissioniController : Controller
    {
        private readonly IMissioniService missioniService;
        public MissioniController(IMissioniService missioniService)
        {
            this.missioniService = missioniService;
        }

        public async Task<IActionResult> Index()
        {
            var missioni = await missioniService.GetMissioniAsync();
            return View(missioni);
        }

        public IActionResult Create()
        {
            return View(new MissioneCreateInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MissioneCreateInputModel inputModel)
        {
            var missione = await missioniService.CreateMissioneAsync(inputModel);
            return RedirectToAction(nameof(Edit), new { id = missione.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inputModel = await missioniService.GetMissioneForEditingAsync(id);
                return View(inputModel);
            }
            catch (PessimisticLockAcquireException)
            {
                // Non posso entrare in modifica perché un altro utente sta modificando, reindirizzo l'utente verso una pagina di attesa
                return RedirectToAction(nameof(WaitForEdit), new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MissioneEditInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await missioniService.EditMissionAsync(inputModel);
                return RedirectToAction(nameof(Index));
            }
            catch (PessimisticLockAcquireException)
            {
                // Non posso aggiornare perché un altro utente sta modificando, reindirizzo l'utente verso una pagina di attesa
                return RedirectToAction(nameof(WaitForEdit), new { id = inputModel.Id });
            }
        }

        public IActionResult WaitForEdit(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> TryAcquireLock(int id)
        {
            try
            {
                await missioniService.RefreshEditingMissioneAsync(id);
                return StatusCode(204);
            }
            catch (PessimisticLockAcquireException)
            {
                return StatusCode(400);
            }
        }
    }
}