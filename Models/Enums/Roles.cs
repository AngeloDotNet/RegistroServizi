using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.Enums
{
    public enum Roles
    {
        [Display(Name = "Amministratore")]
        Administrator,

        [Display(Name = "Gestione Personale")]
        PersonalManagement,

        [Display(Name = "Elenco Servizi")]
        Services,

        [Display(Name = "Configurazione e Manutenzione")]
        ConfigurationMaintenance
    }
}