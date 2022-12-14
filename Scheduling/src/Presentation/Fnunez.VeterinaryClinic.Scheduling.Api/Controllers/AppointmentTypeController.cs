using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class AppointmentTypeController : BaseApiController
{
    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetAppointmentTypesRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesQuery(request);

        GetAppointmentTypesResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}