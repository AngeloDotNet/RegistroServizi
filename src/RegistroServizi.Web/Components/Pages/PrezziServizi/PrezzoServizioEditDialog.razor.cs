using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezzoServizioEditDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public Guid? EditDialogId { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string BtnCancelText { get; set; } = string.Empty;
    [Parameter] public Color BtnCancelColor { get; set; }
    [Parameter] public Color BtnConfirmColor { get; set; }

    private MudForm form = null!;
    private bool isValid = false;

    private CultureInfo localizeCulture = CultureInfo.InvariantCulture;
    private string iconCurrency = string.Empty;

    private Guid tipologiaServizioId = Guid.Empty;
    private string tipologiaServizio = string.Empty;
    private decimal costoFisso = 0m;
    private decimal costoKm = 0m;
    private decimal secondoTrasportato = 0m;
    private decimal fermoMacchina = 0m;
    private decimal? accompagnatore = 0m;
    private int? scontoSocio = 0;

    protected override async Task OnInitializedAsync()
    {
        var cultureName = Configuration[$"{LocalizationCustomOptions.SectionName}:DefaultCulture"] ?? "en-US";
        localizeCulture = new CultureInfo(cultureName);

        iconCurrency = cultureName switch
        {
            "it-IT" => Icons.Material.Filled.EuroSymbol,
            "en-GB" => Icons.Material.Filled.CurrencyPound,
            _ => Icons.Material.Filled.AttachMoney,
        };
        if (EditDialogId.HasValue)
        {
            try
            {
                var item = await PrezzoServizioService.GetPrezzoServizioByIdAsync(EditDialogId.Value);

                if (item != null)
                {
                    tipologiaServizioId = item.TipologiaServizioId;
                    tipologiaServizio = item.TipoServizio;
                    costoFisso = item.CostoFisso;
                    costoKm = item.CostoKm;
                    secondoTrasportato = item.SecondoTrasportato;
                    fermoMacchina = item.FermoMacchina;
                    accompagnatore = item.Accompagnatore;
                    scontoSocio = item.ScontoSocio;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{Localizer["LoadingIdError"]}: {EditDialogId}. {Localizer["Message"]}: {ex.Message}");
                SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["LoadingIdError"]}: {EditDialogId}.");
            }
        }
    }

    private async Task<bool> SubmitForm()
    {
        await form.ValidateAsync();

        if (!isValid)
        {
            // TODO: Remove the localizer and use the validation messages from the form instead
            // Show a notification error message using the SnackbarExtensions
            //SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["AllRequiredFields"]} !");
            return false;
        }

        try
        {
            if (EditDialogId.HasValue)
            {
                var updateItem = new UpdatePrezzoServizioDto(EditDialogId.Value, tipologiaServizioId, costoFisso, costoKm, secondoTrasportato, fermoMacchina, accompagnatore, scontoSocio);

                await PrezzoServizioService.UpdatePrezzoServizioAsync(updateItem);
                SnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityUpdateSuccess"]} !");
            }
            else
            {
                // nessuna azione da fare, perché la creazione di un nuovo prezzo servizio non è supportata in questa versione dell'applicazione
            }

            MudDialog.Close(DialogResult.Ok(true));
            return true;
        }
        catch (Exception ex)
        {
            SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["ErrorOccurred"]}: {ex.Message}");
            return false;
        }
    }

    private void Cancel() => MudDialog.Cancel();
}