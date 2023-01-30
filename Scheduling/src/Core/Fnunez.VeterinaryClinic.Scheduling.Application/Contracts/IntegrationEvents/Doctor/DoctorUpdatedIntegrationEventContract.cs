namespace Contracts;

public class DoctorUpdatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public string DoctorFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
}