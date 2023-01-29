namespace Contracts;

public class RoomDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int RoomId { get; set; }
}