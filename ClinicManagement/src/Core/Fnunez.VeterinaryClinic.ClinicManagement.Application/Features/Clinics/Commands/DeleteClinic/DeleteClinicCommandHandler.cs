using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandHandler : IRequestHandler<DeleteClinicCommand, DeleteClinicResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClinicCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteClinicResponse> Handle(
        DeleteClinicCommand command,
        CancellationToken cancellationToken)
    {
        DeleteClinicRequest request = command.DeleteClinicRequest;
        var response = new DeleteClinicResponse(request.CorrelationId);
        var clinicToDelete = _mapper.Map<Clinic>(request);

        await _unitOfWork
            .Repository<Clinic>()
            .DeleteAsync(clinicToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendIntegrationEventAsync(
            request.Id,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        int clinicId,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new ClinicDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            ClinicId = clinicId
        };

        await _mediator.Publish(
            new ClinicDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}