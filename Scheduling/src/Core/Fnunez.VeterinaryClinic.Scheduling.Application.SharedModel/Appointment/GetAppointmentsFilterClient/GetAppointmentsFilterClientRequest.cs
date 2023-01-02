using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;

public class GetAppointmentsFilterClientRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}