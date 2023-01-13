using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public class GetClinicsFilterAddressQueryHandler
    : IRequestHandler<GetClinicsFilterAddressQuery, GetClinicsFilterAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterAddressResponse> Handle(
        GetClinicsFilterAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterAddressRequest request = query
            .GetClinicsFilterAddressRequest;

        var response = new GetClinicsFilterAddressResponse(
            request.CorrelationId);

        var specification = new ClinicAddressesSpecification(
            request.AddressFilterValue);

        var clinicAddresses = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        if (clinicAddresses is null)
            return response;

        response.ClinicAddresses = clinicAddresses;

        return response;
    }
}