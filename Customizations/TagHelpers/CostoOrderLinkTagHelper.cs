using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RegistroServizi.Models.InputModels.CostiServizi;

namespace RegistroServizi.Customizations.TagHelpers
{
    public class CostoOrderLinkTagHelper : AnchorTagHelper
    {
        public string OrderBy { get; set; }
        public CostoServizioListInputModel Input { get; set; }

        public CostoOrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
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