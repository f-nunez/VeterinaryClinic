namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;

public class AppointmentTypeDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Duration { get; set; }
}