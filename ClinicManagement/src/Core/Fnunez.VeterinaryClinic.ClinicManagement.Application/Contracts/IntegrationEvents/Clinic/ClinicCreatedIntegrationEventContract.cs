namespace Contracts;

public class ClinicCreatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public string ClinicAddress { get; set; } = string.Empty;
    public string ClinicEmailAddress { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
}