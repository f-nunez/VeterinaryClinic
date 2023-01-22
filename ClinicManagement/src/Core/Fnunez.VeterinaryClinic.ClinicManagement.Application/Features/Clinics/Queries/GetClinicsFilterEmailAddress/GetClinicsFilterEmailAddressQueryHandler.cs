using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressQueryHandler
    : IRequestHandler<GetClinicsFilterEmailAddressQuery, GetClinicsFilterEmailAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterEmailAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterEmailAddressResponse> Handle(
        GetClinicsFilterEmailAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterEmailAddressRequest request = query
            .GetClinicsFilterEmailAddressRequest;

        var response = new GetClinicsFilterEmailAddressResponse(
            request.CorrelationId);

        var specification = new ClinicEmailAddressesSpecification(
            request.EmailAddressFilterValue);

        var clinicEmailAddresses = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        if (clinicEmailAddresses is null)
            return response;

        response.ClinicEmailAddresses = clinicEmailAddresses;

        return response;
    }
}