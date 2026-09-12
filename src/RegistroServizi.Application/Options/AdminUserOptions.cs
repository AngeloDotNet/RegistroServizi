using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Application.Options;

public class AdminUserOptions
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}