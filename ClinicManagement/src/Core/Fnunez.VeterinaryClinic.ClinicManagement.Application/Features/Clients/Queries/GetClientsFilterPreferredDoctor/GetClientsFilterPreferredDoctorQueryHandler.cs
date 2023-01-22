using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredDoctor;

public class GetClientsFilterPreferredDoctorQueryHandler
    : IRequestHandler<GetClientsFilterPreferredDoctorQuery, GetClientsFilterPreferredDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterPreferredDoctorQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterPreferredDoctorResponse> Handle(
        GetClientsFilterPreferredDoctorQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterPreferredDoctorRequest request = query
            .GetClientsFilterPreferredDoctorRequest;

        var response = new GetClientsFilterPreferredDoctorResponse(
            request.CorrelationId);

        var specification = new PreferredDoctorFilterValuesSpecification(
            request);

        var preferredDoctorFilterValues = await _unitOfWork
            .ReadRepository<Doctor>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Doctor>()
            .CountAsync(specification, cancellationToken);

        if (preferredDoctorFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<PreferredDoctorFilterValueDto>(
            preferredDoctorFilterValues,
            count
        );

        return response;
    }
}