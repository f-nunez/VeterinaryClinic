namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;

public class AppointmentTypeDto
{
    public int AppointmentTypeId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int Duration { get; set; }
}