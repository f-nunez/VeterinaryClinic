using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;

public class GetAppointmentsFilterRoomRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}