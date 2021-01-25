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
                    message: "La pagina richiesta non esiste.",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),

                AssociazioneNotFoundException exc => new ErrorViewData(
                    message: $"Associazione {exc.AssociazioneId} non trovata",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),
                
                CostoServizioNotFoundException exc => new ErrorViewData(
                    message: $"Costo del servizio {exc.CostoServizioId} non trovato",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),

                ClienteNotFoundException exc => new ErrorViewData(
                    message: $"Cliente {exc.ClienteId} non trovato",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),

                SocioNotFoundException exc => new ErrorViewData(
                    message: $"Socio {exc.SocioId} non trovato",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),
                
                SocioFamiliareNotFoundException exc => new ErrorViewData(
                    message: $"Socio familiare {exc.Id} non trovato",
                    statusCode: HttpStatusCode.NotFound,
                    viewName: "NotFound"),

                RagioneSocialeUnavailableException exc => new ErrorViewData(
                    message: $"La ragione sociale {exc.RagioneSociale} esiste già",
                    statusCode: HttpStatusCode.Conflict,
                    viewName: "Unavailable"),

                SocioUnavailableException exc => new ErrorViewData(
                    message: $"Il socio {exc.Nominativo} esiste già",
                    statusCode: HttpStatusCode.Conflict,
                    viewName: "Unavailable"),

                NominativoUnavailableException exc => new ErrorViewData(
                    message: $"Il nominativo del socio {exc.Nominativo} esiste già",
                    statusCode: HttpStatusCode.Conflict,
                    viewName: "Unavailable"),
                
                OspedaleUnavailableException exc => new ErrorViewData(
                    message: $"L'ospedale {exc.Clinica} esiste già",
                    statusCode: HttpStatusCode.Conflict,
                    viewName: "Unavailable"),

                InvalidAmountException exc => new ErrorViewData(
                    message: $"L'importo non può essere negativo",
                    statusCode: HttpStatusCode.InternalServerError,
                    viewName: "Invalid"),

                _ => new ErrorViewData(message: "")
            };
        }
    }
}