namespace RegistroServizi.Application.Options;

/// <summary>
/// Configuration options that specify an association's full name and abbreviation.
/// </summary>
/// <remarks>Both NomeAssociazione and SiglaAssociazione are required and default to empty strings.</remarks>
public class AssociazioneOptions
{
    [Required] public string NomeAssociazione { get; set; } = string.Empty;
    [Required] public string SiglaAssociazione { get; set; } = string.Empty;
}