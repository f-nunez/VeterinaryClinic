using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterDoctor;

public class GetAppointmentsFilterDoctorQueryHandler
    : IRequestHandler<GetAppointmentsFilterDoctorQuery, GetAppointmentsFilterDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterDoctorQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterDoctorResponse> Handle(
        GetAppointmentsFilterDoctorQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterDoctorRequest request = query
            .GetAppointmentsFilterDoctorRequest;

        var response = new GetAppointmentsFilterDoctorResponse(
            request.CorrelationId);

        var specification = new DoctorFilterValuesSpecification(request);

        var doctorFilterValues = await _unitOfWork
            .ReadRepository<Doctor>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Doctor>()
            .CountAsync(specification, cancellationToken);

        if (doctorFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<DoctorFilterValueDto>(
            doctorFilterValues,
            count
        );

        return response;
    }
}