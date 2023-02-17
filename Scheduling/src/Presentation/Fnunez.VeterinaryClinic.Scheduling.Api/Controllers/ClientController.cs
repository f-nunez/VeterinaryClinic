using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterSalutation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class ClientController : BaseApiController
{
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
    [Authorize("RequiredReaderPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
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
}