namespace Contracts.Scheduling;

public class EmailRequestSchedulingContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
    public string EmailEvent { get; set; } = string.Empty;
    public string SerializedEmailRequest { get; set; } = string.Empty;
}