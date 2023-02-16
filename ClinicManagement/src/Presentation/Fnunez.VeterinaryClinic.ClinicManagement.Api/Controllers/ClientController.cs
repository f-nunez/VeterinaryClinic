using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class ClientController : BaseApiController
{
    [HttpPost("Create")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> Create(
        CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClientCommand(request);

        CreateClientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGrid")]
    [Authorize("RequiredReaderPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterId(
        GetClientsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterIdQuery(request);

        GetClientsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterPreferredDoctor")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterPreferredDoctor(
        GetClientsFilterPreferredDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterPreferredDoctorQuery(request);

        GetClientsFilterPreferredDoctorResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterPreferredName")]
    [Authorize("RequiredReaderPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterSalutation(
        GetClientsFilterSalutationRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsFilterSalutationQuery(request);

        GetClientsFilterSalutationResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{Id}")]
    [Authorize("RequiredWriterPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetClientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery(request);

        GetClientByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetClientDetail")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> GetClientDetail(
        GetClientDetailRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientDetailQuery(request);

        GetClientDetailResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetClientEdit")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> GetClientEdit(
        GetClientEditRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClientEditQuery(request);

        GetClientEditResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("Update")]
    [Authorize("RequiredWriterPolicy")]
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