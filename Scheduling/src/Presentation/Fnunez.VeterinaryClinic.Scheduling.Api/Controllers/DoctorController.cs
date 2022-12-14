using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class DoctorController : BaseApiController
{
    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetDoctorsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsQuery(request);

        GetDoctorsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}