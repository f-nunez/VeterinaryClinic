using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler
    : IRequestHandler<CreateDoctorCommand, CreateDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateDoctorResponse> Handle(
        CreateDoctorCommand command,
        CancellationToken cancellationToken)
    {
        CreateDoctorRequest request = command.CreateDoctorRequest;
        var response = new CreateDoctorResponse(request.CorrelationId);
        var newDoctor = _mapper.Map<Doctor>(request);

        newDoctor = await _unitOfWork
            .Repository<Doctor>()
            .AddAsync(newDoctor, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var doctorDto = _mapper.Map<DoctorDto>(newDoctor);
        response.Doctor = doctorDto;

        await SendIntegrationEventAsync(
            newDoctor,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        Doctor doctor,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new DoctorCreatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            DoctorFullName = doctor.FullName,
            DoctorId = doctor.Id
        };

        await _mediator.Publish(
            new DoctorCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
