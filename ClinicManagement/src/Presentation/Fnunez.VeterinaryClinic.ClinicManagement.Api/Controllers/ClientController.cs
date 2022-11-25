using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class ClientController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClientCommand(request);

        CreateClientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(
        DeleteClientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand(request);

        DeleteClientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetClientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery(request);

        GetClientByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Client is null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetClientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsQuery(request);

        GetClientsResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Clients is null)
            return NotFound();

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateClientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClientCommand(request);

        UpdateClientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}