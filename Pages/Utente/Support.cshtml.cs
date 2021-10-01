using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RegistroServizi.Models.Options;
using RegistroServizi.Models.Services.Application;

namespace RegistroServizi.Pages.Utente
{
     public class SupportModel : PageModel
     {
        private readonly IOptionsMonitor<UsersOptions> _userOptions;

        public SupportModel(IOptionsMonitor<UsersOptions> userOptions)
        {
            _userOptions = userOptions;
        }

        [Required(ErrorMessage = "Il testo della domanda è obbligatorio")]
        [Display(Name = "La tua domanda")]
        [BindProperty]
        public string Question { get; set; }

        public IActionResult OnGetAsync()
        {
            try
            {
                ViewData["Title"] = $"Assistenza";
                return Page();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> onPostAsync([FromServices] IGenericService genericService)
        {
            string SupportEmail = _userOptions.CurrentValue.NotificationEmailSupportRecipient;

            if (ModelState.IsValid)
            {
                await genericService.SendQuestionAsync(SupportEmail, Question);
                
                TempData["ConfirmationMessage"] = "La tua domanda è stata inviata";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return OnGetAsync();
            }
        }
     }
}