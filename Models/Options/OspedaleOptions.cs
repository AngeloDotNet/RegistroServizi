namespace RegistroServizi.Models.Options
{
    public class OspedaleOptions
    {
        public int PerPage { get; set; }
        public OspedaleOrderOptions Order { get; set; }
    }
    public partial class OspedaleOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}