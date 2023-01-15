using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
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

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteClientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand(request);

        DeleteClientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetClientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery(request);

        GetClientByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetClientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsQuery(request);

        GetClientsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterEmailAddress")]
    public async Task<ActionResult> DataGridFilterEmailAddress(
        GetClientsFilterEmailAddressRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterEmailAddressQuery(request);

        GetClientsFilterEmailAddressResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterFullName")]
    public async Task<ActionResult> DataGridFilterFullName(
        GetClientsFilterFullNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterFullNameQuery(request);

        GetClientsFilterFullNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetClientsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterIdQuery(request);

        GetClientsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterPreferredName")]
    public async Task<ActionResult> DataGridFilterPreferredName(
        GetClientsFilterPreferredNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterPreferredNameQuery(request);

        GetClientsFilterPreferredNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterSalutation")]
    public async Task<ActionResult> DataGridFilterSalutation(
        GetClientsFilterSalutationRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterSalutationQuery(request);

        GetClientsFilterSalutationResponse response = await Mediator
            .Send(query, cancellationToken);

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