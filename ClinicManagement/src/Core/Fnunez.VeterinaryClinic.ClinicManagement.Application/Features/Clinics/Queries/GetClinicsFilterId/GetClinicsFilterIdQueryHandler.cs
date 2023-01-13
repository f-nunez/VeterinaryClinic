using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;

public class GetClinicsFilterIdQueryHandler
    : IRequestHandler<GetClinicsFilterIdQuery, GetClinicsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterIdResponse> Handle(
        GetClinicsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterIdRequest request = query
            .GetClinicsFilterIdRequest;

        var response = new GetClinicsFilterIdResponse(request.CorrelationId);
        var specification = new ClinicIdsSpecification(request.IdFilterValue);

        var clinicIds = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        response.ClinicIds = clinicIds;

        return response;
    }
}