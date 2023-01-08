using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AddEditAppointmentComponent : ComponentBase
{
    [Inject]
    private AppointmentService _appointmentService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IJSRuntime _jSRuntime { get; set; }

    [Inject]
    private LayoutSpinnerService _layoutSpinnerService { get; set; }

    #region AppointmentType filter properties
    protected RadzenDropDownDataGrid<int> AppointmentTypeDropDownDataGrid;

    protected int AppointmentTypeFilterCount;

    protected List<AppointmentTypeFilterValueDto> AppointmentTypeFilterValues = new();

    [Parameter]
    public List<AppointmentTypeFilterValueDto> PreselectedAppointmentTypeFilterValues { get; set; }
    #endregion

    #region Doctor filter properties
    protected RadzenDropDownDataGrid<int> DoctorDropDownDataGrid;

    protected int DoctorFilterCount;

    protected List<DoctorFilterValueDto> DoctorFilterValues = new();

    [Parameter]
    public List<DoctorFilterValueDto> PreselectedDoctorFilterValues { get; set; }
    #endregion

    #region Room filter properties
    [Parameter]
    public List<RoomFilterValueDto> PreselectedRoomFilterValues { get; set; }

    protected RadzenDropDownDataGrid<int> RoomDropDownDataGrid;

    protected int RoomFilterCount;

    protected List<RoomFilterValueDto> RoomFilterValues = new();
    #endregion

    protected string DoctorBase64EncodedImageData = string.Empty;

    protected string PatientBase64EncodedImageData = string.Empty;

    [Parameter]
    public AppointmentItemVm Appointment { get; set; }

    [Parameter]
    public bool IsAppointmentToAdd { get; set; } = true;

    [Parameter]
    public string SelectedTimezoneName { get; set; }

    [Parameter]
    public int SelectedTimezoneOffset { get; set; }

    protected override void OnParametersSet()
    {
        DoctorBase64EncodedImageData = AppointmentHelper.GetDemoDoctorPhoto();
        PatientBase64EncodedImageData = AppointmentHelper.GetDemoPatientPhoto();

        if (!IsAppointmentToAdd)
        {
            AppointmentTypeFilterValues = PreselectedAppointmentTypeFilterValues;
            DoctorFilterValues = PreselectedDoctorFilterValues;
            RoomFilterValues = PreselectedRoomFilterValues;
        }
    }

    #region AppointmentType filter methods
    protected async Task AppointmentTypeFilterLoadData(LoadDataArgs args)
    {
        var request = new GetAppointmentsFilterAppointmentTypeRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterAppointmentTypeAsync(request);

        AppointmentTypeFilterValues = dataGridResponse.Items;
        AppointmentTypeFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected void OnChangeAppointmentTypeFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            Appointment.AppointmentTypeId = convertedValue.Value;
        }
        else
        {
            Appointment.AppointmentTypeId = 0;
        }
    }
    #endregion

    #region Doctor filter methods
    protected async Task DoctorFilterLoadData(LoadDataArgs args)
    {
        var request = new GetAppointmentsFilterDoctorRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterDoctorAsync(request);

        DoctorFilterValues = dataGridResponse.Items;
        DoctorFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected void OnChangeDoctorFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            Appointment.DoctorId = convertedValue.Value;
        }
        else
        {
            Appointment.DoctorId = 0;
        }
    }
    #endregion

    #region Room filter methods
    protected async Task RoomFilterLoadData(LoadDataArgs args)
    {
        var request = new GetAppointmentsFilterRoomRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterRoomAsync(request);

        RoomFilterValues = dataGridResponse.Items;
        RoomFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected void OnChangeRoomFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
            Appointment.RoomId = convertedValue.Value;
        else
            Appointment.RoomId = 0;
    }
    #endregion

    protected async void OnSubmit()
    {
        if (!await ValidateIfSelectedDatesAreEarlierThanCurrentAsync())
            return;

        bool isValidAppointment = true;

        _layoutSpinnerService.Show();

        try
        {
            if (IsAppointmentToAdd)
            {
                var request = AppointmentHelper.MapCreateAppointmentRequest(
                    Appointment, SelectedTimezoneOffset);

                await _appointmentService.CreateAppointmentAsync(request);
            }
            else
            {
                var request = AppointmentHelper.MapUpdateAppointmentRequest(
                    Appointment, SelectedTimezoneOffset);

                await _appointmentService.UpdateAppointmentAsync(request);
            }
        }
        catch
        {
            isValidAppointment = false;
        }

        _layoutSpinnerService.Hide();

        if (!isValidAppointment)
            if (IsAppointmentToAdd)
            {
                await _dialogService.Alert(
                    $"The appointment intersects with another",
                    "Add Appointment",
                    new AlertOptions() { OkButtonText = "ACCEPT" }
                );

                return;
            }
            else
            {
                await _dialogService.Alert(
                    $"The appointment intersects with another",
                    "Edit Appointment",
                    new AlertOptions() { OkButtonText = "ACCEPT" }
                );

                return;
            }

        _dialogService.Close(Appointment);
    }

    private async Task<bool> ValidateIfSelectedDatesAreEarlierThanCurrentAsync()
    {
        var userLocalTime = DateTimeOffset.UtcNow
            .ToOffset(TimeSpan.FromMinutes(SelectedTimezoneOffset));

        if (Appointment.StartOn.ToUnspecifiedKind() >= userLocalTime)
            return true;

        _layoutSpinnerService.Hide();

        string titleDialog = IsAppointmentToAdd ? "Add" : "Edit";
        string bodyDialog = IsAppointmentToAdd ? "add" : "edit";

        var isAcceptIt = await _dialogService.Confirm(
            $"The appointment has a start date earlier than the current time, are you sure you want to {bodyDialog} it?",
            $"{titleDialog} Appointment",
            new ConfirmOptions()
            {
                OkButtonText = "Yes",
                CancelButtonText = "No"
            }
        );

        _layoutSpinnerService.Show();

        return isAcceptIt.HasValue ? isAcceptIt.Value : isAcceptIt.HasValue;
    }
}