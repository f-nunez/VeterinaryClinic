using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;

public class UpdateScheduleResponse : BaseResponse
{
    public ScheduleDto Schedule { get; set; } = new ScheduleDto();

    public UpdateScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }
}