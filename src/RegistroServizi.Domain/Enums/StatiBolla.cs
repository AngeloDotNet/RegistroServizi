namespace RegistroServizi.Domain.Enums;

public enum StatiBolla
{
    [Display(Name = "Regolare")]
    Regolare,

    [Display(Name = "Rifiuto Firmato")]
    RifiutoFirmato,

    [Display(Name = "Rifiuto Non Firmato")]
    RifiutoNonFirmato,

    [Display(Name = "Si Allontana")]
    SiAllontana,

    [Display(Name = "Vuoto")]
    Vuoto,

    [Display(Name = "Interrotta")]
    Interrotta,

    [Display(Name = "Evacuato con Elisoccorso")]
    EvacuatoElisoccorso,

    [Display(Name = "Evacuato da altro MSB")]
    EvacuatoAltroMSB,

    [Display(Name = "Deceduto")]
    Deceduto,
}