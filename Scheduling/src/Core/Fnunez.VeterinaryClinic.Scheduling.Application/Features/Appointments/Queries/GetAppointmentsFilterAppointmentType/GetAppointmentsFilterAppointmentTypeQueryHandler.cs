using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;

public class GetAppointmentsFilterAppointmentTypeQueryHandler
    : IRequestHandler<GetAppointmentsFilterAppointmentTypeQuery, GetAppointmentsFilterAppointmentTypeResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterAppointmentTypeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterAppointmentTypeResponse> Handle(
        GetAppointmentsFilterAppointmentTypeQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterAppointmentTypeRequest request = query
            .GetAppointmentsFilterAppointmentTypeRequest;

        var response = new GetAppointmentsFilterAppointmentTypeResponse(
            request.CorrelationId);

        var specification = new AppointmentTypeFilterValuesSpecification(request);

        var appointmentTypeFilterValues = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .CountAsync(specification, cancellationToken);

        if (appointmentTypeFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<AppointmentTypeFilterValueDto>(
            appointmentTypeFilterValues,
            count
        );

        return response;
    }
}