using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypeById;

public class GetAppointmentTypeByIdResponse : BaseResponse
{
    public AppointmentTypeDto AppointmentType { get; set; } = new();

    public GetAppointmentTypeByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}