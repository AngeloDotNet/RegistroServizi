namespace RegistroServizi.Web.Components.Shared.DataGrids;

public partial class DataGridGeneric<T>
{
    // --- PARAMETRI DI INPUT (Da passare dal componente padre) ---
    [Parameter] public IEnumerable<T> Items { get; set; } = []; // Dati da visualizzare nella DataGrid
    [Parameter] public List<string> LayoutWidths { get; set; } = []; // Definizione del Layout (per il ColGroup)
    [Parameter] public List<DataGridColumnDefinition<T>> Columns { get; set; } = []; // Definizione delle Colonne (per le PropertyColumn)
    [Parameter] public bool AddButtonIsDisabled { get; set; } // Flag per disabilitare il pulsante di aggiunta
    [Parameter] public bool ModifyButtonIsDisabled { get; set; } // Flag per disabilitare il pulsante di modifica
    [Parameter] public bool DeleteButtonIsDisabled { get; set; } // Flag per disabilitare il pulsante di eliminazione

    // Callbacks per le azioni CRUD
    [Parameter] public EventCallback ActionCallback { get; set; }
    [Parameter] public EventCallback<T> ActionEditCallback { get; set; }
    [Parameter] public EventCallback<T> ActionDeleteCallback { get; set; }
}