using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;

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