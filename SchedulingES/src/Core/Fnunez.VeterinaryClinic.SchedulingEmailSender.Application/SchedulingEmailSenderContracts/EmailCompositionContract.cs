namespace SchedulingEmailSenderContracts;

public class EmailCompositionContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
    public string SerializedEmailComposition { get; set; } = string.Empty;
}