using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AddEditAppointmentComponent : ComponentBase
{
    [Inject]
    private IAppointmentService _appointmentService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IJSRuntime _jSRuntime { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditAppointmentComponent> StringLocalizer { get; set; }

    #region AppointmentType filter properties
    protected RadzenDropDownDataGrid<int> AppointmentTypeDropDownDataGrid;

    protected int AppointmentTypeFilterCount;

    protected List<AppointmentTypeFilterValueDto> AppointmentTypeFilterValues = new();

    [Parameter]
    public List<AppointmentTypeFilterValueDto> PreselectedAppointmentTypeFilterValues { get; set; }

    protected bool ShowErrorForAppointmentTypeDuration { get; set; }
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

    protected string DoctorPhotoBase64Encoded { get; set; }

    protected string PatientPhotoBase64Encoded { get; set; }

    [Parameter]
    public bool IsAppointmentToAdd { get; set; } = true;

    [Parameter]
    public AddEditAppointmentVm Model { get; set; }

    [Parameter]
    public string SelectedTimezoneName { get; set; }

    [Parameter]
    public int SelectedTimezoneOffset { get; set; }

    protected override void OnInitialized()
    {
        DoctorPhotoBase64Encoded = DoctorHelper.GetDoctorThumbnail();

        PatientPhotoBase64Encoded = Model.PatientPhotoData is null
            ? PatientHelper.GetPatientThumbnail()
            : Convert.ToBase64String(Model.PatientPhotoData);
    }

    protected override void OnParametersSet()
    {
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
            Model.AppointmentTypeId = convertedValue.Value;

            var selectedAppointmentTypeFilterValue = AppointmentTypeFilterValues
                .FirstOrDefault(at => at.Id == convertedValue);

            if (selectedAppointmentTypeFilterValue != null)
                Model.AppointmentTypeDuration = selectedAppointmentTypeFilterValue.Duration;
        }
        else
        {
            Model.AppointmentTypeId = 0;
            Model.AppointmentTypeDuration = 0;
        }

        ValidateAppointmentType();
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
            Model.DoctorId = convertedValue.Value;
        }
        else
        {
            Model.DoctorId = 0;
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
            Model.RoomId = convertedValue.Value;
        else
            Model.RoomId = 0;
    }
    #endregion

    protected void OnChangeEndOnFilter(DateTime? endOn)
    {
        if (endOn != null)
            ValidateAppointmentType();
    }

    protected void OnChangeStartOnFilter(DateTime? startOn)
    {
        if (startOn != null)
            ValidateAppointmentType();
    }

    protected async void OnSubmit()
    {
        if (!ValidateAppointmentType())
            return;

        if (!await ValidateIfSelectedDatesAreEarlierThanCurrentAsync())
            return;

        bool isValidAppointment = true;

        try
        {
            _spinnerService.Show();
            await CreateOrUpdateAppointmentAsync();
        }
        catch
        {
            isValidAppointment = false;
        }
        finally
        {
            _spinnerService.Hide();
        }


        if (!isValidAppointment)
            await ShowConflictingAlertAsync();

        _dialogService.Close(Model);
    }

    private async Task CreateOrUpdateAppointmentAsync()
    {
        if (IsAppointmentToAdd)
        {
            var request = AppointmentHelper.MapCreateAppointmentRequest(
                Model, SelectedTimezoneOffset);

            await _appointmentService.CreateAppointmentAsync(request);
        }
        else
        {
            var request = AppointmentHelper.MapUpdateAppointmentRequest(
                Model, SelectedTimezoneOffset);

            await _appointmentService.UpdateAppointmentAsync(request);
        }
    }

    private async Task ShowConflictingAlertAsync()
    {
        if (IsAppointmentToAdd)
        {
            await ShowAlertAsync(
                StringLocalizer["AddEditAppointment_ConflictingAddAppointment_Alert_Message"],
                StringLocalizer["AddEditAppointment_ConflictingAddAppointment_Alert_Title"],
                StringLocalizer["AddEditAppointment_ConflictingAddAppointment_Alert_Button_Ok"]);

            return;
        }
        else
        {
            await ShowAlertAsync(
                StringLocalizer["AddEditAppointment_ConflictingEditAppointment_Alert_Message"],
                StringLocalizer["AddEditAppointment_ConflictingEditAppointment_Alert_Title"],
                StringLocalizer["AddEditAppointment_ConflictingEditAppointment_Alert_Button_Ok"]);

            return;
        }
    }

    private bool ValidateAppointmentType()
    {
        if (Model.AppointmentTypeDuration > 0)
        {
            TimeSpan difference = Model.EndOn - Model.StartOn;

            var totalMinutes = difference.TotalMinutes;

            if (totalMinutes >= Model.AppointmentTypeDuration)
                ShowErrorForAppointmentTypeDuration = false;
            else
                ShowErrorForAppointmentTypeDuration = true;
        }
        else
            ShowErrorForAppointmentTypeDuration = false;

        return !ShowErrorForAppointmentTypeDuration;
    }

    private async Task<bool> ValidateIfSelectedDatesAreEarlierThanCurrentAsync()
    {
        var userLocalTime = DateTimeOffset.UtcNow
            .ToOffset(TimeSpan.FromMinutes(SelectedTimezoneOffset));

        if (Model.StartOn.ToUnspecifiedKind() >= userLocalTime)
            return true;

        bool? isEarlyDateAccepted;

        if (IsAppointmentToAdd)
        {
            isEarlyDateAccepted = await ShowConfirmationAlertAsync(
                StringLocalizer["AddEditAppointment_EarlyAddAppointment_Alert_Message"],
                StringLocalizer["AddEditAppointment_EarlyAddAppointment_Alert_Title"],
                StringLocalizer["AddEditAppointment_EarlyAddAppointment_Alert_Button_Ok"],
                StringLocalizer["AddEditAppointment_EarlyAddAppointment_Alert_Button_Cancel"]);
        }
        else
        {
            isEarlyDateAccepted = await ShowConfirmationAlertAsync(
                StringLocalizer["AddEditAppointment_EarlyEditAppointment_Alert_Message"],
                StringLocalizer["AddEditAppointment_EarlyEditAppointment_Alert_Title"],
                StringLocalizer["AddEditAppointment_EarlyEditAppointment_Alert_Button_Ok"],
                StringLocalizer["AddEditAppointment_EarlyEditAppointment_Alert_Button_Cancel"]);
        }

        return isEarlyDateAccepted ?? isEarlyDateAccepted.HasValue;
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

    private async Task<bool?> ShowConfirmationAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage,
        string alertButtonCancelMessage)
    {
        return await _dialogService.Confirm(
            alertMessage,
            alertTitle,
            new ConfirmOptions
            {
                OkButtonText = alertButtonOkMessage,
                CancelButtonText = alertButtonCancelMessage
            }
        );
    }
}