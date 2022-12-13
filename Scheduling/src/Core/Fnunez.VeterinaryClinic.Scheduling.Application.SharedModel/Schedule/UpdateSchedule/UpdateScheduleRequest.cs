using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;

public class UpdateScheduleRequest : BaseRequest
{
    public int ClinicId { get; set; }
    public Guid ScheduleId { get; set; }
    public DateTime StartOn { get; set; }
    public DateTime EndOn { get; set; }
}