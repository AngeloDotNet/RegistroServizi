namespace RegistroServizi.Domain.Enums;

public enum TipologiaServizio
{
    [Display(Name = "118")]
    Servizio118 = 1,

    [Display(Name = "Trasporto")]
    Trasporto = 2,

    [Display(Name = "Stazionamento")]
    Stazionamento = 3,

    [Display(Name = "Trasporto Ambulanza")]
    TrasportoAmbulanza = 4,

    [Display(Name = "Automedica")]
    Automedica = 5,

    [Display(Name = "Trasporto Disabili")]
    TrasportoDisabili = 6,

    [Display(Name = "Centro Mobile Rianimazione")]
    CMR = 7,

    [Display(Name = "Guardia Medica")]
    GuardiaMedica = 8,

    [Display(Name = "Trasporto Speciale")]
    TrasportoSpeciale = 9
}