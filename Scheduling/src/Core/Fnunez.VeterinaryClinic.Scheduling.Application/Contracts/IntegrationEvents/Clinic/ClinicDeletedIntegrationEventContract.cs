namespace Contracts;

public class ClinicDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int ClinicId { get; set; }
}