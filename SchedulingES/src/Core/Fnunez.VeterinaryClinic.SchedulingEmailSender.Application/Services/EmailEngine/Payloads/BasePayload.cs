namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;

public abstract class BasePayload
{
    public string? Language { get; set; }
    public string? SendTo { get; set; }
}