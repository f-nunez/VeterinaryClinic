using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandHandler : IRequestHandler<UpdateAppointmentTypeCommand, UpdateAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentTypeCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAppointmentTypeResponse> Handle(
        UpdateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        UpdateAppointmentTypeRequest request = command.UpdateAppointmentTypeRequest;
        var response = new UpdateAppointmentTypeResponse(request.CorrelationId);
        var appointmentTypeToUpdate = _mapper.Map<AppointmentType>(request);

        await _unitOfWork
            .Repository<AppointmentType>()
            .UpdateAsync(appointmentTypeToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentTypeToUpdate);

        await SendIntegrationEventAsync(
            appointmentTypeToUpdate,
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
        var message = new AppointmentTypeUpdatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            AppointmentTypeCode = appointmentType.Code,
            AppointmentTypeDuration = appointmentType.Duration,
            AppointmentTypeId = appointmentType.Id,
            AppointmentTypeName = appointmentType.Name
        };

        await _mediator.Publish(
            new AppointmentTypeUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
