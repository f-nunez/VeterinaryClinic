using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterName;

public class GetClinicsFilterNameQueryHandler
    : IRequestHandler<GetClinicsFilterNameQuery, GetClinicsFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterNameResponse> Handle(
        GetClinicsFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterNameRequest request = query
            .GetClinicsFilterNameRequest;

        var response = new GetClinicsFilterNameResponse(request.CorrelationId);

        var specification = new ClinicNamesSpecification(
            request.NameFilterValue);

        var clinicNames = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        if (clinicNames is null)
            return response;

        response.ClinicNames = clinicNames;

        return response;
    }
}