using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;

public class GetAppointmentAddRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int ClinicId { get; set; }
    public int PatientId { get; set; }
}