using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class AppointmentHelper
{
    public static AddEditAppointmentVm MapAddEditAppointmentViewModel(
        AppointmentAddDto appointmentAddDto,
        DateTime selectedEndOn,
        DateTime selectedStartOn,
        int timezoneOffset)
    {
        var endOn = new DateTimeOffset(
            selectedEndOn.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(timezoneOffset)
        ).DateTime.ToUnspecifiedKind();

        var startOn = new DateTimeOffset(
            selectedStartOn.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(timezoneOffset)
        ).DateTime.ToUnspecifiedKind();

        return new AddEditAppointmentVm
        {
            AppointmentId = default,
            AppointmentTypeDuration = default,
            AppointmentTypeId = default,
            ClientId = appointmentAddDto.ClientId,
            ClientFullName = appointmentAddDto.ClientFullName,
            ClinicId = appointmentAddDto.ClinicId,
            ClinicName = appointmentAddDto.ClinicName,
            Description = string.Empty,
            DoctorId = default,
            EndOn = endOn,
            IsConfirmed = default,
            PatientId = appointmentAddDto.PatientId,
            PatientName = appointmentAddDto.PatientName,
            PatientPhotoData = appointmentAddDto.PatientPhotoData,
            RoomId = default,
            StartOn = startOn,
            Title = string.Empty
        };
    }

    public static AddEditAppointmentVm MapAddEditAppointmentViewModel(
        AppointmentEditDto appointmentEditDto,
        int timezoneOffset)
    {
        var endOn = appointmentEditDto.EndOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        var startOn = appointmentEditDto.StartOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        return new AddEditAppointmentVm
        {
            AppointmentId = appointmentEditDto.AppointmentId,
            AppointmentTypeDuration = appointmentEditDto.AppointmentTypeDuration,
            AppointmentTypeId = appointmentEditDto.AppointmentTypeId,
            ClientId = appointmentEditDto.ClientId,
            ClientFullName = appointmentEditDto.ClientFullName,
            ClinicId = appointmentEditDto.ClinicId,
            ClinicName = appointmentEditDto.ClinicName,
            Description = appointmentEditDto.Description,
            DoctorId = appointmentEditDto.DoctorId,
            EndOn = endOn,
            IsConfirmed = appointmentEditDto.IsConfirmed,
            PatientId = appointmentEditDto.PatientId,
            PatientName = appointmentEditDto.PatientName,
            PatientPhotoData = appointmentEditDto.PatientPhotoData,
            RoomId = appointmentEditDto.RoomId,
            StartOn = startOn,
            Title = appointmentEditDto.Title
        };
    }

    public static AppointmentDetailVm MapAppointmentDetailViewModel(
        AppointmentDetailDto appointmentDto,
        string timezoneName,
        int timezoneOffset)
    {
        var endOn = appointmentDto.EndOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        var startOn = appointmentDto.StartOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        return new AppointmentDetailVm
        {
            AppointmentId = appointmentDto.AppointmentId,
            AppointmentTypeName = appointmentDto.AppointmentTypeName,
            ClientFullName = appointmentDto.ClientFullName,
            ClinicName = appointmentDto.ClinicName,
            Description = appointmentDto.Description,
            DoctorFullName = appointmentDto.DoctorFullName,
            EndOn = endOn,
            IsActive = appointmentDto.IsActive,
            IsConfirmed = appointmentDto.IsConfirmed,
            PatientName = appointmentDto.PatientName,
            PatientPhotoBase64Encoded = Convert.ToBase64String(appointmentDto.PatientPhotoData),
            RoomName = appointmentDto.RoomName,
            StartOn = startOn,
            TimezoneName = timezoneName,
            Title = appointmentDto.Title
        };
    }

    public static List<AppointmentVm> MapAppointmentViewModels(
        List<AppointmentDto> appointmentDtos,
        int timezoneOffset)
    {
        List<AppointmentVm> appointments = new();

        foreach (var appointmentDto in appointmentDtos)
        {
            AppointmentVm appointment = MapAppointmentViewModel(
                appointmentDto, timezoneOffset);

            appointments.Add(appointment);
        }

        return appointments;
    }

    public static CreateAppointmentRequest MapCreateAppointmentRequest(
        AddEditAppointmentVm appointment,
        int timezoneOffset)
    {
        var startOn = new DateTimeOffset(
                appointment.StartOn.ToUnspecifiedKind(),
                TimeSpan.FromMinutes(timezoneOffset)
            );

        var endOn = new DateTimeOffset(
            appointment.EndOn.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(timezoneOffset)
        );

        return new CreateAppointmentRequest
        {
            AppointmentTypeId = appointment.AppointmentTypeId,
            ClientId = appointment.ClientId,
            ClinicId = appointment.ClinicId,
            Description = appointment.Description,
            DoctorId = appointment.DoctorId,
            EndOn = endOn,
            PatientId = appointment.PatientId,
            RoomId = appointment.RoomId,
            StartOn = startOn,
            Title = appointment.Title
        };
    }

    public static UpdateAppointmentRequest MapUpdateAppointmentRequest(
        AddEditAppointmentVm appointment,
        int timezoneOffset)
    {
        var startOn = new DateTimeOffset(
                appointment.StartOn.ToUnspecifiedKind(),
                TimeSpan.FromMinutes(timezoneOffset)
            );

        var endOn = new DateTimeOffset(
            appointment.EndOn.ToUnspecifiedKind(),
            TimeSpan.FromMinutes(timezoneOffset)
        );

        return new UpdateAppointmentRequest
        {
            AppointmentId = appointment.AppointmentId,
            AppointmentTypeId = appointment.AppointmentTypeId,
            Description = appointment.Description,
            DoctorId = appointment.DoctorId,
            EndOn = endOn,
            RoomId = appointment.RoomId,
            StartOn = startOn,
            Title = appointment.Title
        };
    }

    private static AppointmentVm MapAppointmentViewModel(
        AppointmentDto appointmentDto,
        int timezoneOffset)
    {
        var endOn = appointmentDto.EndOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        var startOn = appointmentDto.StartOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset)).DateTime
            .ToUnspecifiedKind();

        return new AppointmentVm
        {
            EndOn = endOn,
            Id = appointmentDto.AppointmentId,
            IsConfirmed = appointmentDto.IsConfirmed,
            StartOn = startOn,
            Title = appointmentDto.Title
        };
    }
}