using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.RadzenTextBoxSearch;

public partial class RadzenTextBoxSearchComponent : FormComponent<string>
{
    /// <summary>
    /// Gets or sets a value indicating the browser built-in autocomplete is enabled.
    /// </summary>
    /// <value><c>true</c> if input automatic complete is enabled; otherwise, <c>false</c>.</value>
    [Parameter]
    public bool AutoComplete { get; set; } = true;

    /// <summary>
    /// Gets or sets the click callback.
    /// </summary>
    /// <value>The click callback.</value>
    [Parameter]
    public EventCallback<MouseEventArgs> Click { get; set; }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    /// <value>The icon.</value>
    [Parameter]
    public string Icon { get; set; } = "search";

    /// <summary>
    /// Gets or sets the maximum allowed text length.
    /// </summary>
    /// <value>The maximum length.</value>
    [Parameter]
    public long? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is read only.
    /// </summary>
    /// <value><c>true</c> if is read only; otherwise, <c>false</c>.</value>
    [Parameter]
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Specifies whether to remove any leading or trailing whitespace from the value. Set to <c>false</c> by default.
    /// </summary>
    /// <value><c>true</c> if trimming is enabled; otherwise, <c>false</c>. </value>
    [Parameter]
    public bool Trim { get; set; }

    /// <summary>
    /// Handles change event of the built-in <c>input</c> elementt.
    /// </summary>
    protected async Task OnChange(ChangeEventArgs args)
    {
        Value = $"{args.Value}";

        if (Trim)
            Value = Value.Trim();

        await ValueChanged.InvokeAsync(Value);

        if (FieldIdentifier.FieldName != null)
            EditContext?.NotifyFieldChanged(FieldIdentifier);

        await Change.InvokeAsync(Value);
    }

    bool clicking;
    /// <summary>
    /// Handles the <see cref="E:Click" /> event.
    /// </summary>
    /// <param name="args">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    public async Task OnClick(MouseEventArgs args)
    {
        if (clicking)
            return;

        try
        {
            clicking = true;

            await Click.InvokeAsync(args);
        }
        finally
        {
            clicking = false;
        }
    }

    /// <inheritdoc />
    protected override string GetComponentCssClass()
    {
        return GetClassList("radzen-text-box-search-container").ToString();
    }
}