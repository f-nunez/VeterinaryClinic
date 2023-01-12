using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;

public class GetAppointmentsFilterPatientRequest : BaseRequest
{
    public int ClientId { get; set; }
}