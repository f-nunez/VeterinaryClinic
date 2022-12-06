using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetSchedules;

public class GetSchedulesResponse : BaseResponse
{
    public List<ScheduleDto> Schedules { get; set; } = new List<ScheduleDto>();
    public int Count { get; set; }

    public GetSchedulesResponse(Guid correlationId) : base(correlationId)
    {
    }
}