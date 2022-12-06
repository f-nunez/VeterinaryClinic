namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;

public class ScheduleDto
{
    public int ClinicId { get; set; }
    public Guid ScheduleId { get; set; }
    public DateTime StartOn { get; set; }
    public DateTime EndOn { get; set; }
    public List<Guid> AppointmentIds { get; set; } = new();
}