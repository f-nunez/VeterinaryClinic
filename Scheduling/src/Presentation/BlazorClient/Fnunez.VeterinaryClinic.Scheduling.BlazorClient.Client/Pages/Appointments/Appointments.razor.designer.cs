using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Extensions;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Appointments;

public partial class AppointmentsComponent : ComponentBase
{
    [Inject]
    private AppointmentService _appointmentService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IJSRuntime _jSRuntime { get; set; }

    #region Client filter properties
    protected List<ClientFilterValueDto> ClientFilterValues = new();

    protected int ClientFilterCount { get; set; }

    protected int? ClientId { get; set; }
    #endregion

    #region Clinic filter properties
    protected List<ClinicFilterValueDto> ClinicFilterValues = new();

    protected int ClinicFilterCount { get; set; }

    protected int? ClinicId { get; set; }

    protected bool IsClinicDropDownEnabled = false;
    #endregion

    #region Patient filter properties
    protected List<PatientFilterValueDto> PatientFilterValues = new();

    protected int? PatientId { get; set; }

    protected string PatientName { get; set; }

    protected bool IsPatientDropDownEnabled = false;
    #endregion

    #region Appointment Scheduler
    private bool _canSchedulerLoadData
    {
        get
        {
            return ClientId.HasValue && PatientId.HasValue && ClinicId.HasValue;
        }
    }

    protected RadzenScheduler<AppointmentVm> Scheduler;

    protected List<AppointmentVm> StoredAppointments = new();
    #endregion

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
        ClinicFilterValues = new();
        PatientFilterValues = new();
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
        }
        else
        {
            ClinicId = null;
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
        ClinicFilterValues = new();
        ClinicFilterCount = 0;
        ClinicId = null;

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
        if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
        {
            args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            return;
        }

        if ((args.View.Text == "Week" || args.View.Text == "Day") &&
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

        var selectedTimezoneOffset = GetSelectedTimezoneOffsetFromDropDown();

        var appointmentToEdit = AppointmentHelper.MapAppointmentItemViewModel(
            response.Appointment, selectedTimezoneOffset);

        var data = await _dialogService.OpenAsync<AddEditAppointment>(
            "Edit Appointment",
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", false },
                { "Appointment", appointmentToEdit },
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

        await _dialogService.Alert(
            $"Appointment {editedAppointment.Title} is successfully edited",
            "Edit Appointment",
            new AlertOptions() { OkButtonText = "ACCEPT" }
        );

        await LoadDataToSchedulerAsync();
    }

    protected async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
    {
        if (ClientId is null)
        {
            await _dialogService.Alert(
                "Please select a client",
                "Appointments",
                new AlertOptions() { OkButtonText = "ACCEPT" }
            );

            return;
        }

        if (PatientId is null)
        {
            await _dialogService.Alert(
                "Please select a patient",
                "Appointments",
                new AlertOptions() { OkButtonText = "ACCEPT" }
            );

            return;
        }

        if (ClinicId is null)
        {
            await _dialogService.Alert(
                "Please select a clinic",
                "Appointments",
                new AlertOptions() { OkButtonText = "ACCEPT" }
            );

            return;
        }

        var selectedTimezoneOffset = GetSelectedTimezoneOffsetFromDropDown();

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
            EndOn = endOn,
            PatientId = PatientId.Value,
            PatientName = PatientName,
            StartOn = startOn
        };

        var data = await _dialogService.OpenAsync<AddEditAppointment>(
            "Add Appointment",
            new Dictionary<string, object>
            {
                { "IsAppointmentToAdd", true},
                { "Appointment", newAppointment },
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

        await _dialogService.Alert(
            $"Appointment {appointment.Title} is successfully added",
            "Add Appointment",
            new AlertOptions() { OkButtonText = "ACCEPT" }
        );

        await LoadDataToSchedulerAsync();
    }

    private async Task<List<AppointmentVm>> GetScheduledAppointmentsAsync()
    {
        var selectedTimezoneOffset = GetSelectedTimezoneOffsetFromDropDown();

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
    #endregion

    private int GetSelectedTimezoneOffsetFromDropDown()
    {
        int dropdownValue = -8;
        int oneHourInMinutes = 60;

        return dropdownValue * oneHourInMinutes;
    }
}