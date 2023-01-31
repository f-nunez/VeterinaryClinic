using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;

public class DeleteAppointmentTypeCommandHandler : IRequestHandler<DeleteAppointmentTypeCommand, DeleteAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentTypeCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentTypeResponse> Handle(
        DeleteAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentTypeRequest request = command
            .DeleteAppointmentTypeRequest;

        var response = new DeleteAppointmentTypeResponse(
            request.CorrelationId);

        var appointmentTypeToDelete = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (appointmentTypeToDelete is null)
            throw new NotFoundException(
                nameof(appointmentTypeToDelete), request.Id);

        await _unitOfWork
            .Repository<AppointmentType>()
            .DeleteAsync(appointmentTypeToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendIntegrationEventAsync(
            appointmentTypeToDelete,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new AppointmentTypeDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            AppointmentTypeId = appointmentType.Id
        };

        await _mediator.Publish(
            new AppointmentTypeDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
