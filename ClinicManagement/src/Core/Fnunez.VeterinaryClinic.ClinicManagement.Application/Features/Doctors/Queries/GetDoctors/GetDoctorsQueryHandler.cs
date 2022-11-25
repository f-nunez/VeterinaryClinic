using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;

public class GetDoctorsQueryHanlder : IRequestHandler<GetDoctorsQuery, GetDoctorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsQueryHanlder(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorsResponse> Handle(
        GetDoctorsQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorsRequest request = query.GetDoctorsRequest;
        var response = new GetDoctorsResponse(request.CorrelationId);
        var specification = new GetDoctorsOrderedByFullNameSpecification();

        var doctors = await _unitOfWork.ReadRepository<Doctor>()
            .ListAsync(specification, cancellationToken);

        if (doctors is null)
            return response;

        response.Doctors = _mapper.Map<List<DoctorDto>>(doctors);
        response.Count = response.Doctors.Count;

        return response;
    }
}