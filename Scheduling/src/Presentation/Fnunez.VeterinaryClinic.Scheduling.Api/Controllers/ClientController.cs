using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class ClientController : BaseApiController
{
    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetClientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsQuery(request);

        GetClientsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}