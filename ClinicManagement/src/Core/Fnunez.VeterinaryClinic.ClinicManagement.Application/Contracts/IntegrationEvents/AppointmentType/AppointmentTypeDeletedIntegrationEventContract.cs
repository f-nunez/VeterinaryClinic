namespace Contracts;

public class AppointmentTypeDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int AppointmentTypeId { get; set; }
}