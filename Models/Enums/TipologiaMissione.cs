using System.ComponentModel;

namespace RegistroServizi.Models.Enums
{
    public enum TipologiaMissione
    {
        [Description("118")]
        Centodiciotto,

        [Description("Dimissione")]
        Dimissione,

        [Description("Dialisi")]
        Dialisi
    }
}