using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        public HomeController(IApplicationPersister applicationPersister)
        {
            this.applicationPersister = applicationPersister;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View();
        }

        [AllowAnonymous]
        public IActionResult Licenza()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View();
        }

        public IActionResult Dashboard()
        {
            ViewData["Title"] = applicationPersister.GetTitoloApp();
            return View();
        }
    }
}