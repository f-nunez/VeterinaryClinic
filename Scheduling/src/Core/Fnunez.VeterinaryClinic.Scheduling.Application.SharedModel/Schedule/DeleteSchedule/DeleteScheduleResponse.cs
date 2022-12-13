using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;

public class DeleteScheduleResponse : BaseResponse
{
    public DeleteScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }
}