using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterClient;

public class GetPatientsFilterClientQueryHandler
    : IRequestHandler<GetPatientsFilterClientQuery, GetPatientsFilterClientResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsFilterClientQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientsFilterClientResponse> Handle(
        GetPatientsFilterClientQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientsFilterClientRequest request = query
            .GetPatientsFilterClientRequest;

        var response = new GetPatientsFilterClientResponse(
            request.CorrelationId);

        var specification = new ClientFilterValuesSpecification(request);

        var clientFilterValues = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Client>()
            .CountAsync(specification, cancellationToken);

        if (clientFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<ClientFilterValueDto>(
            clientFilterValues,
            count
        );

        return response;
    }
}
