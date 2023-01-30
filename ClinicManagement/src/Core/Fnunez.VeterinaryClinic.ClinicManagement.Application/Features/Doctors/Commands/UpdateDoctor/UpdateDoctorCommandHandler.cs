using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, UpdateDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDoctorCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateDoctorResponse> Handle(
        UpdateDoctorCommand command,
        CancellationToken cancellationToken)
    {
        UpdateDoctorRequest request = command.UpdateDoctorRequest;
        var response = new UpdateDoctorResponse(request.CorrelationId);
        var doctorToUpdate = _mapper.Map<Doctor>(request);

        await _unitOfWork.Repository<Doctor>()
            .UpdateAsync(doctorToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var doctorDto = _mapper.Map<DoctorDto>(doctorToUpdate);
        response.Doctor = doctorDto;

        await SendIntegrationEventAsync(
            doctorToUpdate,
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
        var message = new DoctorUpdatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            DoctorFullName = doctor.FullName,
            DoctorId = doctor.Id
        };

        await _mediator.Publish(
            new DoctorUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}