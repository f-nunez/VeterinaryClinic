using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;

public class CreateAppointmentTypeCommandHandler
    : IRequestHandler<CreateAppointmentTypeCommand, CreateAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentTypeCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateAppointmentTypeResponse> Handle(
        CreateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        CreateAppointmentTypeRequest request = command
            .CreateAppointmentTypeRequest;

        var response = new CreateAppointmentTypeResponse(
            request.CorrelationId);

        var newAppointemntType = _mapper.Map<AppointmentType>(request);

        newAppointemntType = await _unitOfWork
            .Repository<AppointmentType>()
            .AddAsync(newAppointemntType, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(newAppointemntType);

        await SendIntegrationEventAsync(
            newAppointemntType,
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
        var message = new AppointmentTypeCreatedIntegrationEventContract
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
            new AppointmentTypeCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
