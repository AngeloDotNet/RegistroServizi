namespace RegistroServizi.Models.Options
{
    public partial class CostoServizioOptions
    {
        public int PerPage { get; set; }
        public CostoServizioOrderOptions Order { get; set; }
    }
    public partial class CostoServizioOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}