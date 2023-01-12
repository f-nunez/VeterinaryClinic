using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AppointmentsComponent : ComponentBase
{
    [Inject]
    private IAppointmentService _appointmentService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IUserSettingsService _userSettingsService { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditAppointmentComponent> StringLocalizerForAddEditAppointment { get; set; }

    #region Client filter properties
    protected List<ClientFilterValueDto> ClientFilterValues = new();

    protected int ClientFilterCount { get; set; }

    protected int? ClientId { get; set; }
    #endregion

    #region Clinic filter properties
    protected List<ClinicFilterValueDto> ClinicFilterValues = new();

    protected int ClinicFilterCount { get; set; }

    protected int? ClinicId { get; set; }

    protected string ClinicName { get; set; }

    protected bool IsClinicDropDownEnabled = false;
    #endregion

    #region Patient filter properties
    protected List<PatientFilterValueDto> PatientFilterValues = new();

    protected int? PatientId { get; set; }

    protected string PatientName { get; set; }

    protected bool IsPatientDropDownEnabled = false;
    #endregion

    #region Appointment Scheduler properties
    private bool _canSchedulerLoadData
    {
        get
        {
            return ClientId.HasValue && PatientId.HasValue && ClinicId.HasValue;
        }
    }

    private string _schedulerTextDay { get; set; }
    private string _schedulerTextMonth { get; set; }
    private string _schedulerTextWeek { get; set; }
    private DateTime _userTodayDateTime { get; set; }

    protected RadzenScheduler<AppointmentVm> Scheduler;

    protected List<AppointmentVm> StoredAppointments = new();
    #endregion

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _schedulerTextDay = StringLocalizer["Appointments_Scheduler_Text_Day"];
        _schedulerTextMonth = StringLocalizer["Appointments_Scheduler_Text_Month"];
        _schedulerTextWeek = StringLocalizer["Appointments_Scheduler_Text_Week"];

        int selectedTimezoneOffset = await _userSettingsService
            .GetUtcOffsetInMinutesAsync();

        _userTodayDateTime = DateTimeOffset.UtcNow
            .ToOffset(TimeSpan.FromMinutes(selectedTimezoneOffset)).Date;
    }

    #region Client filter methods
    protected async Task ClientFilterLoadData(LoadDataArgs args)
    {
        var request = new GetAppointmentsFilterClientRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterClientAsync(request);

        ClientFilterValues = dataGridResponse.Items;
        ClientFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnChangeClientFilter(object value)
    {
        ClinicId = null;
        ClinicName = string.Empty;
        PatientId = null;
        PatientName = string.Empty;

        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            ClientId = convertedValue;
            await PatientFilterLoadData(ClientId.Value);
            IsPatientDropDownEnabled = true;
        }
        else
        {
            IsClinicDropDownEnabled = false;
            IsPatientDropDownEnabled = false;
        }

        await LoadDataToSchedulerAsync();
    }
    #endregion

    #region Clinic filter methods
    protected async Task ClinicFilterLoadData(LoadDataArgs args)
    {
        var request = new GetAppointmentsFilterClinicRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterClinicAsync(request);

        ClinicFilterValues = dataGridResponse.Items;
        ClinicFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnChangeClinicFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            ClinicId = convertedValue;
            ClinicName = ClinicFilterValues
                .FirstOrDefault(c => c.Id == ClinicId.Value)!.Name;
        }
        else
        {
            ClinicId = null;
            ClinicName = string.Empty;
        }

        await LoadDataToSchedulerAsync();
    }
    #endregion

    #region Patient filter methods
    protected async Task PatientFilterLoadData(int clientId)
    {
        var request = new GetAppointmentsFilterPatientRequest
        {
            ClientId = clientId
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterPatientAsync(request);

        PatientFilterValues = dataGridResponse;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnChangePatientFilter(object value)
    {
        ClinicFilterCount = 0;
        ClinicId = null;
        ClinicName = string.Empty;

        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            PatientId = convertedValue;

            PatientName = PatientFilterValues
                .FirstOrDefault(x => x.Id == PatientId.Value)?.Name;

            IsClinicDropDownEnabled = true;
        }
        else
        {
            PatientId = null;
            PatientName = string.Empty;
            IsClinicDropDownEnabled = false;
        }

        await LoadDataToSchedulerAsync();
    }
    #endregion

    #region Appointment Scheduler methods
    protected async Task SchedulerLoadData(SchedulerLoadDataEventArgs args)
    {
        await LoadDataToSchedulerAsync();
    }

    protected void OnSlotRender(SchedulerSlotRenderEventArgs args)
    {
        if ((args.View.Text == _schedulerTextMonth) && args.Start.Date == _userTodayDateTime)
        {
            args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            return;
        }

        if ((args.View.Text == _schedulerTextWeek || args.View.Text == _schedulerTextDay) &&
            args.Start.Hour >= 8 && args.Start.Hour <= 19)
        {
            args.Attributes["style"] = "background: rgba(255,220,40,.2);";
        }
    }

    protected void OnAppointmentRender(
        SchedulerAppointmentRenderEventArgs<AppointmentVm> args)
    {
        // Never call StateHasChanged in AppointmentRender coz would lead to infinite loop
        if (args.Data.IsConfirmed)
            args.Attributes["style"] = "background: lightgreen; color: black;";
    }

    protected async Task OnAppointmentSelect(
        SchedulerAppointmentSelectEventArgs<AppointmentVm> args)
    {
        var request = new GetAppointmentDetailRequest
        {
            AppointmentId = args.Data.Id
        };

        var response = await _appointmentService
            .GetAppointmentDetailAsync(request);

        var selectedTimezoneName = await _userSettingsService.GetTimeZoneNameAsync();
        var selectedTimezoneOffset = await _userSettingsService.GetUtcOffsetInMinutesAsync();

        var appointmentToEdit = AppointmentHelper.MapAppointmentItemViewModel(
            response.Appointment, selectedTimezoneOffset);

        var data = await _dialogService.OpenAsync<AddEditAppointment>(
            StringLocalizerForAddEditAppointment["AddEditAppointment_Label_EditAppointment"],
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", false },
                { "Appointment", appointmentToEdit },
                { "SelectedTimezoneName", selectedTimezoneName },
                { "SelectedTimezoneOffset", selectedTimezoneOffset },
                { "PreselectedAppointmentTypeFilterValues", response.AppointmentTypeFilterValues },
                { "PreselectedDoctorFilterValues", response.DoctorFilterValues },
                { "PreselectedRoomFilterValues", response.RoomFilterValues }
            },
            new DialogOptions
            {
                CssClass = "col-sm-12 col-md-10 col-lg-8",
                Width = "unset"
            }
        );

        if (data is null)
            return;

        var editedAppointment = data as AppointmentItemVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Appointments_EditedAppointment_Alert_Message"], editedAppointment.Title),
            StringLocalizer["Appointments_EditedAppointment_Alert_Title"],
            StringLocalizer["Appointments_EditedAppointment_Alert_Button_Ok"]);

        await LoadDataToSchedulerAsync();
    }

    protected async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
    {
        bool canProceedToSchedule = await ValidateRequiredFieldsToScheduleAndAppointmentAsync();

        if (!canProceedToSchedule)
            return;

        var selectedTimezoneName = await _userSettingsService.GetTimeZoneNameAsync();
        var selectedTimezoneOffset = await _userSettingsService.GetUtcOffsetInMinutesAsync();

        var endOn = new DateTimeOffset(
            args.End.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        ).DateTime.ToUnspecifiedKind();

        var startOn = new DateTimeOffset(
            args.Start.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        ).DateTime.ToUnspecifiedKind();

        var newAppointment = new AppointmentItemVm
        {
            ClientId = ClientId.Value,
            ClinicId = ClinicId.Value,
            ClinicName = ClinicName,
            EndOn = endOn,
            PatientId = PatientId.Value,
            PatientName = PatientName,
            StartOn = startOn
        };

        var data = await _dialogService.OpenAsync<AddEditAppointment>(
            StringLocalizerForAddEditAppointment["AddEditAppointment_Label_AddAppointment"],
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", true},
                { "Appointment", newAppointment },
                { "SelectedTimezoneName", selectedTimezoneName },
                { "SelectedTimezoneOffset", selectedTimezoneOffset }
            },
            new DialogOptions
            {
                CssClass = "col-sm-12 col-md-10 col-lg-8",
                Width = "unset"
            }
        );

        if (data is null)
            return;

        var appointment = data as AppointmentItemVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Appointments_AddedAppointment_Alert_Message"], appointment.Title),
            StringLocalizer["Appointments_AddedAppointment_Alert_Title"],
            StringLocalizer["Appointments_AddedAppointment_Alert_Button_Ok"]);

        await LoadDataToSchedulerAsync();
    }

    private async Task<List<AppointmentVm>> GetScheduledAppointmentsAsync()
    {
        var selectedTimezoneOffset = await _userSettingsService.GetUtcOffsetInMinutesAsync();

        var startOnWithOffset = new DateTimeOffset(
            Scheduler.SelectedView.StartDate.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        );

        var endOnWithOffset = new DateTimeOffset(
            Scheduler.SelectedView.EndDate.AddTicks(-1).ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        );

        var request = new GetAppointmentsRequest
        {
            ClientIdFilterValue = $"{ClientId}",
            ClinicIdFilterValue = $"{ClinicId}",
            EndOn = endOnWithOffset,
            PatientIdFilterValue = $"{PatientId}",
            StartOn = startOnWithOffset
        };

        var response = await _appointmentService.DataGridAsync(request);

        return AppointmentHelper.MapAppointmentViewModels(
            response.Items, selectedTimezoneOffset);
    }

    private async Task LoadDataToSchedulerAsync()
    {
        if (_canSchedulerLoadData)
            StoredAppointments = await GetScheduledAppointmentsAsync();
        else
            StoredAppointments = new();

        await InvokeAsync(StateHasChanged);
    }

    private async Task<bool> ValidateRequiredFieldsToScheduleAndAppointmentAsync()
    {
        if (ClientId is null)
        {
            await ShowAlertAsync(
                StringLocalizer["Appointments_Client_Alert_Message"],
                StringLocalizer["Appointments_Client_Alert_Title"],
                StringLocalizer["Appointments_Client_Alert_Button_Ok"]);

            return false;
        }

        if (PatientId is null)
        {
            await ShowAlertAsync(
                StringLocalizer["Appointments_Patient_Alert_Message"],
                StringLocalizer["Appointments_Patient_Alert_Title"],
                StringLocalizer["Appointments_Patient_Alert_Button_Ok"]);

            return false;
        }

        if (ClinicId is null)
        {
            await ShowAlertAsync(
                StringLocalizer["Appointments_Clinic_Alert_Message"],
                StringLocalizer["Appointments_Clinic_Alert_Title"],
                StringLocalizer["Appointments_Clinic_Alert_Button_Ok"]);

            return false;
        }

        return true;
    }

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions()
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
    #endregion
}