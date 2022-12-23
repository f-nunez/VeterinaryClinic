using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Rooms;

public partial class RoomsFilter : ComponentBase
{
    [Inject]
    private RoomService _roomService { get; set; }
    protected IEnumerable<string> RoomIds;
    protected IEnumerable<string> RoomNames;
    [Inject]
    protected DialogService DialogService { get; set; }
    protected string IdFilterValue { get; set; }
    protected string NameFilterValue { get; set; }
    [Parameter]
    public RoomsFilterValues RoomsFilterValues { get; set; }

    protected override void OnInitialized()
    {
        IdFilterValue = RoomsFilterValues.IdFilterValue;
        NameFilterValue = RoomsFilterValues.NameFilterValue;
    }

    protected void OnClickButtonFilter()
    {
        var filterValues = new RoomsFilterValues
        {
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        DialogService.CloseSide(filterValues);
    }

    protected void OnClickButtonClean()
    {
        var filterValues = new RoomsFilterValues
        {
            IdFilterValue = string.Empty,
            NameFilterValue = string.Empty
        };

        DialogService.CloseSide(filterValues);
    }

    protected async void OnChangeId(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            RoomIds = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            RoomNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async Task LoadDataRoomsFilterId(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            RoomIds = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        RoomIds = await _roomService
            .DataGridFilterIdAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataRoomsFilterName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        RoomNames = await _roomService
            .DataGridFilterNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }
}