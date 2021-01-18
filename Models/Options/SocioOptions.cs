namespace RegistroServizi.Models.Options
{
    public class SocioOptions
    {
        public int PerPage { get; set; }
        public SocioOrderOptions Order { get; set; }
    }
    
    public partial class SocioOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}