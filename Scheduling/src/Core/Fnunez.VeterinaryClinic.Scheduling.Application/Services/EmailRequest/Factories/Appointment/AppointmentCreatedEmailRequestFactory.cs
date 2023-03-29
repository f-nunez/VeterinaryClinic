using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Requests;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;

public class AppointmentCreatedEmailRequestFactory : IEmailRequestFactory
{
    private readonly Appointment _appointment;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public AppointmentCreatedEmailRequestFactory(
        Appointment appointment,
        Guid correlationId,
        string? userId)
    {
        _appointment = appointment;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseEmailRequest CreateEmailRequest()
    {
        return new AppointmentCreatedEmailRequest
        {
            AppointmentEndOn = _appointment.DateRange.EndOn,
            AppointmentId = _appointment.Id,
            AppointmentStartOn = _appointment.DateRange.StartOn,
            ClientFullName = _appointment.Client.FullName,
            ClinicAddress = _appointment.Clinic.Address,
            ClinicName = _appointment.Clinic.Name,
            CorrelationId = _correlationId,
            DoctorFullName = _appointment.Doctor.FullName,
            PatientName = _appointment.Patient.Name,
            Language = _appointment.Client.PreferredLanguage.ToString(),
            SendTo = _appointment.Client.EmailAddress,
            TriggeredByUserId = _userId
        };
    }

    public string GetEmailEvent()
    {
        return EmailEvent.AppointmentCreated.ToString();
    }
}