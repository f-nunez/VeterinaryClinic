using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;

public interface IEmailRequestService
{
    Task CreateAndSendAsync(IEmailRequestFactory factory, CancellationToken cancellationToken);
}