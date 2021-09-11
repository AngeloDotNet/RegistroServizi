using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.Enums;
using RegistroServizi.Models.InputModels.Utenti;

namespace RegistroServizi.Pages.Admin
{
    [Authorize(Roles = nameof(Roles.Administrator))]
    public class UsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public UserRoleInputModel Input { get; set; }
        public IList<ApplicationUser> Users { get; private set; }

        [BindProperty(SupportsGet = true)]
        public Roles InRole { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Title"] = "Gestione utenti";
            Claim claim = new (ClaimTypes.Role, InRole.ToString());
            Users = await userManager.GetUsersForClaimAsync(claim);
            
            return Page();
        }

        public IActionResult OnPostAssignAsync()
        {
            throw new Exception();
        }

        public IActionResult OnPostRevokeAsync()
        {
            throw new Exception();
        }
    }
}