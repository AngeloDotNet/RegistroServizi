using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Models.Services.Application
{
    public class EfCoreGenericService : IGenericService
    {
        private readonly ILogger<EfCoreGenericService> logger;
        private readonly RegistroServiziDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailClient emailClient;

        public EfCoreGenericService(IHttpContextAccessor httpContextAccessor, ILogger<EfCoreGenericService> logger, IEmailClient emailClient, RegistroServiziDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.dbContext = dbContext;
            this.emailClient = emailClient;
        }

        public async Task SendQuestionAsync(string appEmail, string question)
        {
            string userFullName;
            string userEmail;

            userFullName = httpContextAccessor.HttpContext.User.FindFirst("FullName").Value;
            userEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            question = new HtmlSanitizer(allowedTags: new string[0]).Sanitize(question);

            string subject = $@"Domanda di supporto dal portale Admin Area";
            string message = $@"<p>L'utente {userFullName} (<a href=""{userEmail}"">{userEmail}</a>) ti ha inviato la seguente domanda:</p>
                                <p>{question}</p>";

            try
            {
                await emailClient.SendEmailAsync(appEmail, userEmail, subject, message);
            }
            catch
            {
                //TODO: Implementare errore customizzato
                throw new Exception();
            }
        }
    }
}