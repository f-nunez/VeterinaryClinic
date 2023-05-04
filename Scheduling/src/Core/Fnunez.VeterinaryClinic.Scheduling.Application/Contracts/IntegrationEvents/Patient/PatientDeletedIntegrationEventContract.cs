namespace Contracts;

public class PatientDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int PatientId { get; set; }
}