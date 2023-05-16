using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int ClinicId { get; set; }
    public int PatientId { get; set; }
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }
}