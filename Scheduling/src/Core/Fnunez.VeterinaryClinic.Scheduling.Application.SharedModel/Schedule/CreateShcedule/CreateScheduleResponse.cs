using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;

public class CreateScheduleResponse : BaseResponse
{
    public ScheduleDto Schedule { get; set; } = new ScheduleDto();

    public CreateScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }
}