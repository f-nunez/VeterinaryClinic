using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;

public class CreateScheduleRequest : BaseRequest
{
    public int ClinicId { get; set; }
    public DateTime StartOn { get; set; }
    public DateTime EndOn { get; set; }
}