using Microsoft.AspNetCore.Http;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public interface IErrorViewSelectorService
    {
        ErrorViewData GetErrorViewData(HttpContext context);
    }
}