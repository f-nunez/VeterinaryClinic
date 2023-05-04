using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class AppointmentController : BaseApiController
{
    [HttpPost("Create")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> Create(
        CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAppointmentCommand(request);

        CreateAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGrid")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGrid(
        GetAppointmentsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsQuery(request);

        GetAppointmentsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterAppointmentType")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterAppointmentType(
        GetAppointmentsFilterAppointmentTypeRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterAppointmentTypeQuery(request);

        GetAppointmentsFilterAppointmentTypeResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterClient")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterClient(
        GetAppointmentsFilterClientRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterClientQuery(request);

        GetAppointmentsFilterClientResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterClinic")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterClinic(
        GetAppointmentsFilterClinicRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterClinicQuery(request);

        GetAppointmentsFilterClinicResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterDoctor")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterDoctor(
        GetAppointmentsFilterDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterDoctorQuery(request);

        GetAppointmentsFilterDoctorResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterPatient")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterPatient(
        GetAppointmentsFilterPatientRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterPatientQuery(request);

        GetAppointmentsFilterPatientResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterRoom")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> DataGridFilterRoom(
        GetAppointmentsFilterRoomRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsFilterRoomQuery(request);

        GetAppointmentsFilterRoomResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{AppointmentId}")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAppointmentCommand(request);

        DeleteAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetAppointmentAdd")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> GetAppointmentAdd(
        GetAppointmentAddRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentAddQuery(request);

        GetAppointmentAddResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetAppointmentDetail")]
    [Authorize("RequiredReaderPolicy")]
    public async Task<ActionResult> GetAppointmentDetail(
        GetAppointmentDetailRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentDetailQuery(request);

        GetAppointmentDetailResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetAppointmentEdit")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> GetAppointmentEdit(
        GetAppointmentEditRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentEditQuery(request);

        GetAppointmentEditResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("Update")]
    [Authorize("RequiredWriterPolicy")]
    public async Task<ActionResult> Update(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAppointmentCommand(request);

        UpdateAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}