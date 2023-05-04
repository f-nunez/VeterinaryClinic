namespace Contracts;

public class ClientDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int ClientId { get; set; }
}