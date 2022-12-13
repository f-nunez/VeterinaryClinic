using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetScheduleById;

public class GetScheduleByIdRequest : BaseRequest
{
    public Guid Id { get; set; }
}