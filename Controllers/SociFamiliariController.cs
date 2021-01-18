using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Services.Application.SociFamiliari;
using RegistroServizi.Models.Services.Infrastructure;

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
    }
}