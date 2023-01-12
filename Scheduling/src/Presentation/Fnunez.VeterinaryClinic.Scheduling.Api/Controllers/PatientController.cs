using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class PatientController : BaseApiController
{
    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetPatientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientsQuery(request);

        GetPatientsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}