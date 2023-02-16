namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Handlers;

public class AntiforgeryHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("X-CSRF", "1");

        return base.SendAsync(request, cancellationToken);
    }
}