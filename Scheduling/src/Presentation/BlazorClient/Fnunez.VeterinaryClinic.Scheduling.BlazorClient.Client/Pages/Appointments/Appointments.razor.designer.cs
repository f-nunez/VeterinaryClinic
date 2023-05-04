using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
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
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IUserSettingsService _userSettingsService { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditAppointmentComponent> StringLocalizerForAddEditAppointment { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentDetailComponent> StringLocalizerForAppointmentDetail { get; set; }

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
        _spinnerService.Show();

        var request = new GetAppointmentsFilterClientRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterClientAsync(request);

        ClientFilterValues = dataGridResponse.Items;
        ClientFilterCount = dataGridResponse.Count;

        _spinnerService.Hide();

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
        _spinnerService.Show();

        var request = new GetAppointmentsFilterClinicRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterClinicAsync(request);

        ClinicFilterValues = dataGridResponse.Items;
        ClinicFilterCount = dataGridResponse.Count;

        _spinnerService.Hide();

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
        _spinnerService.Show();

        var request = new GetAppointmentsFilterPatientRequest
        {
            ClientId = clientId
        };

        var dataGridResponse = await _appointmentService
            .DataGridFilterPatientAsync(request);

        PatientFilterValues = dataGridResponse;

        _spinnerService.Hide();

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
        if ((args.View.Text == _schedulerTextMonth) &&
            args.Start.Date == _userTodayDateTime)
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
        _spinnerService.Show();

        var request = new GetAppointmentDetailRequest
        {
            AppointmentId = args.Data.Id
        };

        var response = await _appointmentService
            .GetAppointmentDetailAsync(request);

        var selectedTimezoneName = await _userSettingsService
            .GetTimeZoneNameAsync();

        var selectedTimezoneOffset = await _userSettingsService
            .GetUtcOffsetInMinutesAsync();

        var appointmentDetail = AppointmentHelper.MapAppointmentDetailViewModel(
            response.Appointment, selectedTimezoneName, selectedTimezoneOffset);

        _spinnerService.Hide();

        var appointmentDetailData = await ShowDialogForAppointmentDetailAsync(appointmentDetail);

        if (appointmentDetailData is null)
            return;

        var appointmentDetailResponse = (AppointmentDetailResponse)appointmentDetailData;

        switch (appointmentDetailResponse)
        {
            case AppointmentDetailResponse.Delete:
                await PerformActionToDeleteAppointmentAsync(
                    args.Data.Id, appointmentDetail.Title);
                break;

            case AppointmentDetailResponse.Edit:
                await PerformActionToEditAppointmentAsync(
                    args.Data.Id, selectedTimezoneName, selectedTimezoneOffset);
                break;

            default:
                throw new ArgumentException(
                    $"Not found the value: {appointmentDetailResponse}");
        }

        await LoadDataToSchedulerAsync();
    }

    protected async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
    {
        bool canProceedToSchedule = await ValidateRequiredFieldsToPerformAnAppointmentAsync();

        if (!canProceedToSchedule)
            return;

        var selectedTimezoneName = await _userSettingsService
            .GetTimeZoneNameAsync();

        var selectedTimezoneOffset = await _userSettingsService
            .GetUtcOffsetInMinutesAsync();

        _spinnerService.Show();

        var request = new GetAppointmentAddRequest
        {
            ClientId = ClientId.Value,
            ClinicId = ClinicId.Value,
            PatientId = PatientId.Value
        };

        var response = await _appointmentService.GetAppointmentAddAsync(request);

        var newAppointment = AppointmentHelper.MapAddEditAppointmentViewModel(
            response.Appointment,
            args.End,
            args.Start,
            selectedTimezoneOffset
        );

        _spinnerService.Hide();

        var data = await ShowDialogToAddAsync(
            newAppointment,
            selectedTimezoneName,
            selectedTimezoneOffset
        );

        if (data is null)
            return;

        var appointment = data as AddEditAppointmentVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Appointments_AddedAppointment_Alert_Message"], appointment.Title),
            StringLocalizer["Appointments_AddedAppointment_Alert_Title"],
            StringLocalizer["Appointments_AddedAppointment_Alert_Button_Ok"]);

        await LoadDataToSchedulerAsync();
    }

    private async Task<List<AppointmentVm>> GetScheduledAppointmentsAsync()
    {
        var selectedTimezoneOffset = await _userSettingsService
            .GetUtcOffsetInMinutesAsync();

        var startOnWithOffset = new DateTimeOffset(
            Scheduler.SelectedView.StartDate.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        );

        var endOnWithOffset = new DateTimeOffset(
            Scheduler.SelectedView.EndDate.AddTicks(-1).ToUnspecifiedKind(),
            TimeSpan.FromMinutes(selectedTimezoneOffset)
        );

        _spinnerService.Show();

        var request = new GetAppointmentsRequest
        {
            ClientIdFilterValue = $"{ClientId}",
            ClinicIdFilterValue = $"{ClinicId}",
            EndOn = endOnWithOffset,
            PatientIdFilterValue = $"{PatientId}",
            StartOn = startOnWithOffset
        };

        var response = await _appointmentService.DataGridAsync(request);

        _spinnerService.Hide();

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

    private async Task PerformActionToDeleteAppointmentAsync(
        Guid appointmentId,
        string appointmentTitle)
    {
        _spinnerService.Show();

        var request = new DeleteAppointmentRequest
        {
            AppointmentId = appointmentId
        };

        await _appointmentService.DeleteAppointmentAsync(request);

        _spinnerService.Hide();

        await ShowAlertAsync(
            string.Format(StringLocalizer["Appointments_DeletedAppointment_Alert_Message"], appointmentTitle),
            StringLocalizer["Appointments_DeletedAppointment_Alert_Title"],
            StringLocalizer["Appointments_DeletedAppointment_Alert_Button_Ok"]);
    }

    private async Task PerformActionToEditAppointmentAsync(
        Guid appointmentId,
        string timezoneName,
        int timezoneOffset)
    {
        _spinnerService.Show();

        var requestEdit = new GetAppointmentEditRequest
        {
            AppointmentId = appointmentId
        };

        var responseEdit = await _appointmentService
            .GetAppointmentEditAsync(requestEdit);

        var appointmentToEdit = AppointmentHelper.MapAddEditAppointmentViewModel(
            responseEdit.Appointment, timezoneOffset);

        _spinnerService.Hide();

        var appointmentEditData = await ShowDialogToEditAsync(
            appointmentToEdit,
            responseEdit.AppointmentTypeFilterValues,
            responseEdit.DoctorFilterValues,
            responseEdit.RoomFilterValues,
            timezoneName,
            timezoneOffset
        );

        if (appointmentEditData is null)
            return;

        var editedAppointment = appointmentEditData as AddEditAppointmentVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Appointments_EditedAppointment_Alert_Message"], editedAppointment.Title),
            StringLocalizer["Appointments_EditedAppointment_Alert_Title"],
            StringLocalizer["Appointments_EditedAppointment_Alert_Button_Ok"]);
    }

    private async Task<dynamic> ShowDialogForAppointmentDetailAsync(
        AppointmentDetailVm appointmentDetail)
    {
        return await _dialogService.OpenAsync<AppointmentDetail>(
            StringLocalizerForAppointmentDetail["AppointmentDetail_Label_AppointmentDetail"],
            new Dictionary<string, object>
            {
                { "Model", appointmentDetail }
            },
            new DialogOptions
            {
                CssClass = "col-md-10 col-lg-8",
                Width = "unset"
            }
        );
    }

    private async Task<dynamic> ShowDialogToAddAsync(
        AddEditAppointmentVm appointment,
        string timezoneName,
        int timezoneOffset)
    {
        return await _dialogService.OpenAsync<AddEditAppointment>(
            StringLocalizerForAddEditAppointment["AddEditAppointment_Label_AddAppointment"],
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", true},
                { "Model", appointment },
                { "SelectedTimezoneName", timezoneName },
                { "SelectedTimezoneOffset", timezoneOffset }
            },
            new DialogOptions
            {
                CssClass = "col-md-10 col-lg-8",
                Width = "unset"
            }
        );
    }

    private async Task<dynamic> ShowDialogToEditAsync(
        AddEditAppointmentVm appointment,
        List<AppointmentTypeFilterValueDto> appointmentTypeFilterValues,
        List<DoctorFilterValueDto> doctorFilterValues,
        List<RoomFilterValueDto> roomFilterValues,
        string timezoneName,
        int timezoneOffset)
    {
        return await _dialogService.OpenAsync<AddEditAppointment>(
            StringLocalizerForAddEditAppointment["AddEditAppointment_Label_EditAppointment"],
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", false },
                { "Model", appointment },
                { "SelectedTimezoneName", timezoneName },
                { "SelectedTimezoneOffset", timezoneOffset },
                { "PreselectedAppointmentTypeFilterValues", appointmentTypeFilterValues },
                { "PreselectedDoctorFilterValues", doctorFilterValues },
                { "PreselectedRoomFilterValues", roomFilterValues }
            },
            new DialogOptions
            {
                CssClass = "col-md-10 col-lg-8",
                Width = "unset"
            }
        );
    }

    private async Task<bool> ValidateRequiredFieldsToPerformAnAppointmentAsync()
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
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
    #endregion
}