namespace Fnunez.VeterinaryClinic.Public.Web.Services.Appointment;

public interface IAppointmentService
{
    Task ConfirmAppointmentAsync(string encryptedAppointmentId);
}