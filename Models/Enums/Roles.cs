using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.Enums
{
    public enum Roles
    {
        [Display(Name = "Amministratore")]
        amministratore,

        [Display(Name = "Gestione Personale")]
        personale,

        [Display(Name = "Elenco Servizi")]
        servizi,

        [Display(Name = "Configurazione e Manutenzione")]
        manutenzione
    }
}