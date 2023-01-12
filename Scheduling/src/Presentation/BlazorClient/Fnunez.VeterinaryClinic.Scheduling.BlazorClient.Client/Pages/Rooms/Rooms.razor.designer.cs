using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Rooms;

public partial class RoomsComponent : ComponentBase
{
    [Inject]
    private IRoomService _roomService { get; set; }

    protected RadzenDataGrid<RoomDto> RoomsGrid;

    protected List<RoomDto> Rooms;

    protected int Count;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<RoomsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<RoomsFilterComponent> StringLocalizerForFilter { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string CodeFilterValue { get; set; }

    protected string DurationFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;
        var request = new GetRoomsRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _roomService
            .DataGridAsync(request);

        Count = dataGridResponse.Count;
        Rooms = dataGridResponse.Items;
        IsLoading = false;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickFilterSearch()
    {
        await ResetGridAndSearchAsync();
    }

    protected async Task OnKeyUpFilterSearch(KeyboardEventArgs args)
    {
        if (args.Key != "Enter")
            return;

        await ResetGridAndSearchAsync();
    }

    protected async Task OpenFilterMenu()
    {
        var filterValues = new RoomsFilterValues
        {
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(RoomsFilterValues), filterValues }
        };

        var result = await DialogService.OpenSideAsync<RoomsFilter>(
            StringLocalizerForFilter["RoomsFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(RoomsFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        IdFilterValue = filterValues.IdFilterValue;
        NameFilterValue = filterValues.NameFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        RoomsGrid.Reset(false);
        await RoomsGrid.FirstPage(true);
    }
}