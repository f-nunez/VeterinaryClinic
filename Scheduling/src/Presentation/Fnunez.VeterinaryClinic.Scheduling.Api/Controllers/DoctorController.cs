using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorById;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class DoctorController : BaseApiController
{
    [HttpPost("DataGrid")]
    [Authorize("RequiredReaderPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
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
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterId(
        GetDoctorsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsFilterIdQuery(request);

        GetDoctorsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetDoctorByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorByIdQuery(request);

        GetDoctorByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}