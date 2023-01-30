namespace Contracts;

public class DoctorCreatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public string DoctorFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
}