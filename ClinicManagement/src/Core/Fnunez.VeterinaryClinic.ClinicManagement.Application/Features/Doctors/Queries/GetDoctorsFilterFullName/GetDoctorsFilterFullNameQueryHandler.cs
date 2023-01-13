using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameQueryHandler
    : IRequestHandler<GetDoctorsFilterFullNameQuery, GetDoctorsFilterFullNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsFilterFullNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorsFilterFullNameResponse> Handle(
        GetDoctorsFilterFullNameQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorsFilterFullNameRequest request = query
            .GetDoctorsFilterFullNameRequest;

        var response = new GetDoctorsFilterFullNameResponse(
            request.CorrelationId);

        var specification = new DoctorFullNamesSpecification(
            request.FullNameFilterValue);

        var doctorFullNames = await _unitOfWork
            .ReadRepository<Doctor>()
            .ListAsync(specification, cancellationToken);

        if (doctorFullNames is null)
            return response;

        response.DoctorFullNames = doctorFullNames;

        return response;
    }
}