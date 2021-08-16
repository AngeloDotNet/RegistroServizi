using Microsoft.AspNetCore.Identity;

namespace RegistroServizi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
    public string FullName { get; set; }
    }
}