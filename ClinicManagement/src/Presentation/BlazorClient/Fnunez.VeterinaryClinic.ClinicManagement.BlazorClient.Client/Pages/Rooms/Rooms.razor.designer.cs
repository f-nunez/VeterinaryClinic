using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Rooms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Rooms;

public partial class RoomsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IRoomService _roomService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditRoomComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<RoomDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<RoomsFilterComponent> _stringLocalizerForFilter { get; set; }

    protected bool CanWrite { get; set; }

    protected RadzenDataGrid<RoomDto> RoomsGrid;

    protected List<RoomDto> Rooms;

    protected int Count;

    [Inject]
    protected IStringLocalizer<RoomsComponent> StringLocalizer { get; set; }

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string CodeFilterValue { get; set; }

    protected string DurationFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        _spinnerService.Show();
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
        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditRoom>(
            _stringLocalizerForAdd["AddEditRoom_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsRoomToAdd", true }
            }
        );

        if (response is null)
            return;

        var savedRoom = response as RoomVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Rooms_AddedRoom_Alert_Message"], savedRoom.Name),
            StringLocalizer["Rooms_AddedRoom_Alert_Title"],
            StringLocalizer["Rooms_AddedRoom_Alert_Button_Ok"]);

        await RoomsGrid.Reload();
    }

    protected async Task OnClickDelete(RoomDto room)
    {
        string message = string.Format(
            StringLocalizer["Rooms_DeleteRoom_Alert_Message"],
            room.Name);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["Rooms_DeleteRoom_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["Rooms_DeleteRoom_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["Rooms_DeleteRoom_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        _spinnerService.Show();

        var request = new DeleteRoomRequest
        {
            Id = room.Id
        };

        await _roomService.DeleteAsync(request);

        _spinnerService.Hide();

        await ShowAlertAsync(
            string.Format(StringLocalizer["Rooms_DeletedRoom_Alert_Message"], room.Name),
            StringLocalizer["Rooms_DeletedRoom_Alert_Title"],
            StringLocalizer["Rooms_DeletedRoom_Alert_Button_Ok"]);

        await RoomsGrid.ReloadAfterDeleteItemAsync();
    }

    protected async Task OnClickDetail(RoomDto room)
    {
        _spinnerService.Show();

        var request = new GetRoomByIdRequest
        {
            Id = room.Id
        };

        var currentRoom = await _roomService.GetByIdAsync(request);

        var roomForDetail = RoomHelper.MapRoomViewModel(room);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<RoomDetail>(
            _stringLocalizerForDetail["RoomDetail_Label_RoomDetail"],
            new Dictionary<string, object>
            {
                { "Model", roomForDetail }
            }
        );
    }

    protected async Task OnClickEdit(RoomDto room)
    {
        _spinnerService.Show();

        var request = new GetRoomByIdRequest
        {
            Id = room.Id
        };

        var currentRoom = await _roomService.GetByIdAsync(request);

        var roomToEdit = RoomHelper.MapRoomViewModel(room);

        _spinnerService.Hide();

        var response = await _dialogService.OpenAsync<AddEditRoom>(
            _stringLocalizerForAdd["AddEditRoom_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsRoomToAdd", false },
                { "Model", roomToEdit }
            }
        );

        if (response is null)
            return;

        var savedRoom = response as RoomVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Rooms_EditedRoom_Alert_Message"], savedRoom.Name),
            StringLocalizer["Rooms_EditedRoom_Alert_Title"],
            StringLocalizer["Rooms_EditedRoom_Alert_Button_Ok"]);

        await RoomsGrid.Reload();
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

        var result = await _dialogService.OpenSideAsync<RoomsFilter>(
            _stringLocalizerForFilter["RoomsFilter_Label_Filter"],
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

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
}