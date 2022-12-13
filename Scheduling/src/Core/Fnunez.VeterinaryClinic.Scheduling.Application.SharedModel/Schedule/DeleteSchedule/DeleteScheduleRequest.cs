using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;

public class DeleteScheduleRequest : BaseRequest
{
    public Guid Id { get; set; }
}