namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;

public class AppointmentTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string Name { get; set; } = string.Empty;
}