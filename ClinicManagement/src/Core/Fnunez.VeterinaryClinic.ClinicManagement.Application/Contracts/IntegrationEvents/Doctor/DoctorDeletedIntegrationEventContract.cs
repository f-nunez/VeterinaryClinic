namespace Contracts;

public class DoctorDeletedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public int DoctorId { get; set; }
}