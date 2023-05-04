using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorQueryHandler
    : IRequestHandler<GetPatientsFilterPreferredDoctorQuery, GetPatientsFilterPreferredDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsFilterPreferredDoctorQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientsFilterPreferredDoctorResponse> Handle(
        GetPatientsFilterPreferredDoctorQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientsFilterPreferredDoctorRequest request = query
            .GetPatientsFilterPreferredDoctorRequest;

        var response = new GetPatientsFilterPreferredDoctorResponse(
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