namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;

public class ClinicDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Address { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}