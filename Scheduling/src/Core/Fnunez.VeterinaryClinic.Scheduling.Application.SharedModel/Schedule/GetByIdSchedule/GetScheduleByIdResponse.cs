using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetScheduleById;

public class GetScheduleByIdResponse : BaseResponse
{
    public ScheduleDto Schedule { get; set; } = new ScheduleDto();

    public GetScheduleByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}