namespace Contracts;

public class RoomCreatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
}