using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RegistroServizi.Models.InputModels.Clienti;

namespace RegistroServizi.Customizations.TagHelpers
{
    public class ClienteOrderLinkTagHelper : AnchorTagHelper
    {
        public string OrderBy { get; set; }
        public ClienteListInputModel Input { get; set; }

        public ClienteOrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            RouteValues["search"] = Input.Search;
            RouteValues["orderby"] = OrderBy;
            RouteValues["ascending"] = (Input.OrderBy == OrderBy ? !Input.Ascending : Input.Ascending).ToString().ToLowerInvariant();
            
            base.Process(context, output);

            if (Input.OrderBy == OrderBy)
            {
                var direction = Input.Ascending ? "up" : "down";
                output.PostContent.SetHtmlContent($" <i class=\"fas fa-caret-{direction}\"></i>");
            }
        }
    }
}