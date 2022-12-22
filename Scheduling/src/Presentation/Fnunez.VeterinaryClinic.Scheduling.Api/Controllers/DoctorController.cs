using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class DoctorController : BaseApiController
{
    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetDoctorsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsQuery(request);

        GetDoctorsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterFullName")]
    public async Task<ActionResult> DataGridFilterFullName(
        GetDoctorsFilterFullNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsFilterFullNameQuery(request);

        GetDoctorsFilterFullNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetDoctorsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsFilterIdQuery(request);

        GetDoctorsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}