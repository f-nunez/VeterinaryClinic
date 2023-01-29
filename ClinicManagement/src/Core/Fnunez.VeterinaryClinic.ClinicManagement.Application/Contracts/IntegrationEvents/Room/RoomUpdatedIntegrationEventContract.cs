namespace Contracts;

public class RoomUpdatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
}