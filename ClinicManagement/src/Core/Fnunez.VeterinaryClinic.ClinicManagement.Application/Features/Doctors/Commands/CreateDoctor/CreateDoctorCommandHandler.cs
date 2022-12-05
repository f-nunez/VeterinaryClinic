using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, CreateDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateDoctorResponse> Handle(
        CreateDoctorCommand command,
        CancellationToken cancellationToken)
    {
        CreateDoctorRequest request = command.CreateDoctorRequest;
        var response = new CreateDoctorResponse(request.CorrelationId);
        var newDoctor = _mapper.Map<Doctor>(request);

        newDoctor = await _unitOfWork.Repository<Doctor>()
            .AddAsync(newDoctor, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var doctorDto = _mapper.Map<DoctorDto>(newDoctor);
        response.Doctor = doctorDto;

        return response;
    }
}
