using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, DeleteDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteDoctorResponse> Handle(
        DeleteDoctorCommand command,
        CancellationToken cancellationToken)
    {
        DeleteDoctorRequest request = command.DeleteDoctorRequest;
        var response = new DeleteDoctorResponse(request.CorrelationId);
        var doctorToDelete = _mapper.Map<Doctor>(request);

        await _unitOfWork.Repository<Doctor>()
            .DeleteAsync(doctorToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendIntegrationEventAsync(
            request.Id,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        int doctorId,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new DoctorDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            DoctorId = doctorId
        };

        await _mediator.Publish(
            new DoctorDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
