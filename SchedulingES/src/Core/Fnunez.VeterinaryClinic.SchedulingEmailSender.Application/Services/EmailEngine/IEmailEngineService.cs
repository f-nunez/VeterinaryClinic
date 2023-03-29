namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine;

public interface IEmailEngineService
{
    public Task CreateAndSendAsync(string emailEventString, string serializedEmailRequest, CancellationToken cancellationToken);
}