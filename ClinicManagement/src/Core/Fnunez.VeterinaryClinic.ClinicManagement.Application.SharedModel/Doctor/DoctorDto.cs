namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;

public class DoctorDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string FullName { get; set; } = string.Empty;
}