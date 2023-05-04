using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Doctors;

public partial class DoctorsFilterComponent : ComponentBase
{
    [Inject]
    private IDoctorService _roomService { get; set; }

    protected IEnumerable<string> DoctorFullNames;

    protected IEnumerable<string> DoctorIds;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<DoctorsFilterComponent> StringLocalizer { get; set; }

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    [Parameter]
    public DoctorsFilterValues DoctorsFilterValues { get; set; }

    protected override void OnInitialized()
    {
        FullNameFilterValue = DoctorsFilterValues.FullNameFilterValue;
        IdFilterValue = DoctorsFilterValues.IdFilterValue;
    }

    protected void OnClickButtonFilter()
    {
        var filterValues = new DoctorsFilterValues
        {
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue
        };

        DialogService.CloseSide(filterValues);
    }

    protected void OnClickButtonClean()
    {
        var filterValues = new DoctorsFilterValues
        {
            FullNameFilterValue = string.Empty,
            IdFilterValue = string.Empty
        };

        DialogService.CloseSide(filterValues);
    }

    protected async void OnChangeFullName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            DoctorFullNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeId(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            DoctorIds = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async Task LoadDataDoctorsFilterFullName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        DoctorFullNames = await _roomService
            .DataGridFilterFullNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataDoctorsFilterId(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            DoctorIds = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        DoctorIds = await _roomService
            .DataGridFilterIdAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }
}