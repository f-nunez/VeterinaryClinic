using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterCodeQuery, GetAppointmentTypesFilterCodeResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterCodeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterCodeResponse> Handle(
        GetAppointmentTypesFilterCodeQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterCodeRequest request = query
            .GetAppointmentTypesFilterCodeRequest;

        var response = new GetAppointmentTypesFilterCodeResponse(
            request.CorrelationId);

        var specification = new AppointmentTypeCodesSpecification(
            request.CodeFilterValue);

        var appointmentTypeCodes = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        if (appointmentTypeCodes is null)
            return response;

        response.AppointemntTypeCodes = appointmentTypeCodes;

        return response;
    }
}