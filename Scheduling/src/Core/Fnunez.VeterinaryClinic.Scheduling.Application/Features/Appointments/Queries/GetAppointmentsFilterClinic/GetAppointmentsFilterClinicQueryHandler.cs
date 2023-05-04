using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClinic;

public class GetAppointmentsFilterClinicQueryHandler
    : IRequestHandler<GetAppointmentsFilterClinicQuery, GetAppointmentsFilterClinicResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterClinicQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterClinicResponse> Handle(
        GetAppointmentsFilterClinicQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterClinicRequest request = query
            .GetAppointmentsFilterClinicRequest;

        var response = new GetAppointmentsFilterClinicResponse(
            request.CorrelationId);

        var specification = new ClinicFilterValuesSpecification(request);

        var clinicFilterValues = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Clinic>()
            .CountAsync(specification, cancellationToken);

        if (clinicFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<ClinicFilterValueDto>(
            clinicFilterValues,
            count
        );

        return response;
    }
}