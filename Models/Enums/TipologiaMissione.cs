using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.Enums
{
    public enum TipologiaMissione
    {
        [Display(Name = "118")]
        Emergenza,

        [Display(Name = "Trasporto")]
        Trasporto,

        [Display(Name = "Trasporto ambulanza")]
        TrasportoAmbulanza,

        [Display(Name = "Trasporto Disabili")]
        TrasportoDisabili,

        [Display(Name = "Trasporto Speciale")]
        TrasportoSpeciale,

        [Display(Name = "Stazionamento")]
        Stazionamento,

        [Display(Name = "Automedica")]
        Automedica,

        [Display(Name = "Centro Mobile Rianimazione")]
        CMR,

        [Display(Name = "Guardia Medica")]
        GuardiaMedica
    }
}