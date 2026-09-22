namespace RegistroServizi.Application.Options;

/// <summary>
/// Configuration options that specify an association's full name and abbreviation.
/// </summary>
/// <remarks>Both NomeAssociazione and SiglaAssociazione are required and default to empty strings.</remarks>
public class AssociazioneOptions
{
    /// <summary>
    /// Name of the association.
    /// </summary>
    /// <remarks>Marked with [Required] for model validation; defaults to an empty string.</remarks>
    [Required] public string NomeAssociazione { get; set; } = string.Empty;

    /// <summary>
    /// Abbreviation of the association's name.
    /// </summary>
    /// <remarks>Marked with [Required] for model validation; defaults to an empty string.</remarks>
    [Required] public string SiglaAssociazione { get; set; } = string.Empty;
}