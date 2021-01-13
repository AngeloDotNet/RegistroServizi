namespace RegistroServizi.Models.Options
{
    public class ClienteOptions
    {
        public int PerPage { get; set; }
        public ClienteOrderOptions Order { get; set; }
    }

    public partial class ClienteOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}