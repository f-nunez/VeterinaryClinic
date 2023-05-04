namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;

public abstract class BaseEmailRequest
{
    public Guid CorrelationId { get; set; }
    public string? Language { get; set; }
    public string? SendTo { get; set; }
    public string? TriggeredByUserId { get; set; }
}