using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler
    : IRequestHandler<GetDoctorByIdQuery, GetDoctorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorByIdResponse> Handle(
        GetDoctorByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorByIdRequest request = query.GetDoctorByIdRequest;
        var response = new GetDoctorByIdResponse(request.CorrelationId);

        var doctor = await _unitOfWork.ReadRepository<Doctor>()
            .GetByIdAsync(request.Id);

        if (doctor is null)
            throw new NotFoundException(nameof(doctor), request.Id);

        response.Doctor = _mapper.Map<DoctorDto>(doctor);

        return response;
    }
}