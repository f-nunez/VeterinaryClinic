using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public class GetDoctorsFilterIdQueryHandler
    : IRequestHandler<GetDoctorsFilterIdQuery, GetDoctorsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorsFilterIdResponse> Handle(
        GetDoctorsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorsFilterIdRequest request = query.GetDoctorsFilterIdRequest;
        var response = new GetDoctorsFilterIdResponse(request.CorrelationId);
        var specification = new DoctorIdsSpecification(request.IdFilterValue);

        var doctorIds = await _unitOfWork
            .ReadRepository<Doctor>()
            .ListAsync(specification, cancellationToken);

        if (doctorIds is null)
            return response;

        response.DoctorIds = doctorIds;

        return response;
    }
}