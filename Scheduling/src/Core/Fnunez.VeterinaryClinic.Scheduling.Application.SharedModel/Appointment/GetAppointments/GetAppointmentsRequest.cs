using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int ClinicId { get; set; }
    public int PatientId { get; set; }
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }

    public override string ToString()
    {
        return $"ClientId: {ClientId}, ClinicId: {ClinicId}, PatientId: {PatientId}, StartOn: {StartOn.ToString()}, EndOn: {EndOn.ToString()}";
    }
}