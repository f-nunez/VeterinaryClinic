using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.CustomCard;

public partial class CustomCardComponent : ComponentBase
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public string? Text { get; set; }

    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(Class))
            Class = "customcard";
        else
            Class = $"customcard {Class}";

        if (string.IsNullOrEmpty(Text))
            Text = string.Empty;
    }
}