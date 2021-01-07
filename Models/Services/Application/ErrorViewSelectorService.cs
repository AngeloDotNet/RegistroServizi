using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RegistroServizi.Models.Exceptions.Application;
using RegistroServizi.Models.Exceptions.Infrastructure;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.Services.Application
{
    public class ErrorViewSelectorService : IErrorViewSelectorService
    {
        public ErrorViewData GetErrorViewData(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            
            return exception switch
            {
                null => new ErrorViewData(
                    //message: $"Pagina '{context.Request.Path}' non trovata",
                    message: "La pagina richiesta non esiste.",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),

                AssociazioneNotFoundException exc => new ErrorViewData(
                    message: $"Associazione {exc.AssociazioneId} non trovata",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "AssociazioneNotFound"),
                
                CostoServizioNotFoundException exc => new ErrorViewData(
                    message: $"Costo del servizio {exc.CostoServizioId} non trovato",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "CostoServizioNotFound"),

                InvalidAmountException exc => new ErrorViewData(
                    message: $"L'importo non può essere negativo",
                    statusCode: HttpStatusCode.InternalServerError,
                    viewName: "InvalidAmount"),

                _ => new ErrorViewData(message: "")
            };
        }
    }
}