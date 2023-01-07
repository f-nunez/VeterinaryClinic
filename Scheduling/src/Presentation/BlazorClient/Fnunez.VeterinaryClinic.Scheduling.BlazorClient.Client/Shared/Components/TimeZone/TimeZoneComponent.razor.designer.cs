using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;

public partial class TimeZoneComponent : ComponentBase
{
    private bool _isCompletedFirstRenderer = false;

    private bool _isPreSelectionApplied = false;

    [Inject]
    private ITimeZoneComponentService _timeZoneComponentService { get; set; }

    protected IEnumerable<TimeZone> TimeZones { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string SelectedTimeZoneId { get; set; }

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
        TimeZones = _timeZoneComponentService.GetTimeZones();
    }

    protected override void OnParametersSet()
    {
        if (_isCompletedFirstRenderer && !_isPreSelectionApplied)
        {
            _timeZoneComponentService.SetTimeZone(SelectedTimeZoneId);
            Value = SelectedTimeZoneId;
            _isPreSelectionApplied = true;
        }
    }
}