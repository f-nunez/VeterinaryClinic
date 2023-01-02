using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;

public class GetAppointmentsFilterRoomResponse : BaseResponse
{
    public DataGridResponse<RoomFilterValueDto>? DataGridResponse { get; set; }

    public GetAppointmentsFilterRoomResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}