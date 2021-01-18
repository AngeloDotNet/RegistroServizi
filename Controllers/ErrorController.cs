using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        public ErrorController(IApplicationPersister applicationPersister)
        {
            this.applicationPersister= applicationPersister;
        }
        
        public IActionResult Index([FromServices] IErrorViewSelectorService errorViewSelectorService)
        {
            ErrorViewData errorViewData = errorViewSelectorService.GetErrorViewData(HttpContext);

            ViewData["Title"] = applicationPersister.GetTitoloApp();
            ViewData["TitleMessage"] = errorViewData.Title;
            ViewData["Message"] = errorViewData.Message;

            Response.StatusCode = (int) errorViewData.StatusCode;
            return View(errorViewData.ViewName);
        }
    }
}