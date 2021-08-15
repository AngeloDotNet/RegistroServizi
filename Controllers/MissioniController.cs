using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.Services.Application.Missioni;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Controllers
{
    public class MissioniController : Controller
    {
        private readonly IApplicationPersister applicationPersister;
        private readonly IMissioniService missioni;
        public MissioniController(IApplicationPersister applicationPersister, IMissioniService missioni)
        {
            this.applicationPersister= applicationPersister;
            this.missioni = missioni;
        }

    }
}