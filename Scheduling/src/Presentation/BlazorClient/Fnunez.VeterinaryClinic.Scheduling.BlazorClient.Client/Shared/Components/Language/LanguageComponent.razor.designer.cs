using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Language;

public partial class LanguageComponent : ComponentBase
{
    private bool _isCompletedFirstRenderer = false;

    private bool _isPreSelectionApplied = false;

    [Inject]
    private ILanguageComponentService _languageComponentService { get; set; }

    [Inject]
    protected IStringLocalizer<LanguageComponent> StringLocalizer { get; set; }

    protected IEnumerable<Language> Languages { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string SelectedCultureCode { get; set; }

    [Parameter]
    public string Value { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    protected async Task OnChange(object value)
    {
        if (value is null)
            return;

        await ValueChanged.InvokeAsync((string)value);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            _isCompletedFirstRenderer = firstRender;
    }

    protected override void OnInitialized()
    {
        Languages = _languageComponentService.GetLanguages();
    }

    protected override void OnParametersSet()
    {
        if (_isCompletedFirstRenderer && !_isPreSelectionApplied)
        {
            _languageComponentService.SetLanguage(SelectedCultureCode);
            Value = SelectedCultureCode;
            _isPreSelectionApplied = true;
        }
    }
}