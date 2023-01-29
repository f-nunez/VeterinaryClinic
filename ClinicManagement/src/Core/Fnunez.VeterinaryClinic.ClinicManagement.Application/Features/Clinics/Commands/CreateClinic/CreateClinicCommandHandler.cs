using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandHandler : IRequestHandler<CreateClinicCommand, CreateClinicResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClinicCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateClinicResponse> Handle(
        CreateClinicCommand command,
        CancellationToken cancellationToken)
    {
        CreateClinicRequest request = command.CreateClinicRequest;
        var response = new CreateClinicResponse(request.CorrelationId);
        var newClinic = _mapper.Map<Clinic>(request);

        newClinic = await _unitOfWork
            .Repository<Clinic>()
            .AddAsync(newClinic, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Clinic = _mapper.Map<ClinicDto>(newClinic);

        await SendIntegrationEventAsync(
            newClinic,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new ClinicCreatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            ClinicAddress = clinic.Address,
            ClinicEmailAddress = clinic.EmailAddress,
            ClinicId = clinic.Id,
            ClinicName = clinic.Name
        };

        await _mediator.Publish(
            new ClinicCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}