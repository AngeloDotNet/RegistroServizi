namespace RegistroServizi.Web.Components.Shared.DataGrids;

public partial class DataGridGeneric<T>
{
    // --- PARAMETRI DI INPUT (Da passare dal componente padre) ---
    [Parameter] public IEnumerable<T> Items { get; set; } = []; // Dati da visualizzare nella DataGrid
    //[Parameter] public SortMode SortMode { get; set; } = SortMode.None;
    //[Parameter] public bool Bordered { get; set; } = true;
    //[Parameter] public bool Dense { get; set; } = false;
    //[Parameter] public bool Hover { get; set; } = false;
    [Parameter] public List<string> LayoutWidths { get; set; } = []; // Definizione del Layout (per il ColGroup)
    [Parameter] public List<DataGridColumnDefinition<T>> Columns { get; set; } = []; // Definizione delle Colonne (per le PropertyColumn)

    // Callbacks per le azioni CRUD
    [Parameter] public EventCallback ActionCallback { get; set; }
    [Parameter] public EventCallback<T> ActionEditCallback { get; set; }
    [Parameter] public EventCallback<T> ActionDeleteCallback { get; set; }

    // --- STRUTTURE INTERNE ---
    //public class ColumnDefinition
    //{
    //    // store an expression that selects the property from T
    //    public Expression<Func<T, object>> Property { get; set; }
    //    public string Title { get; set; } = string.Empty; // Etichetta visuale
    //}
}