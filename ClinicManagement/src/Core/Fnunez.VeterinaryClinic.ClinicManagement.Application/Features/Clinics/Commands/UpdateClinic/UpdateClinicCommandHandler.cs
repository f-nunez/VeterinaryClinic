using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandHandler
    : IRequestHandler<UpdateClinicCommand, UpdateClinicResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClinicResponse> Handle(
        UpdateClinicCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClinicRequest request = command.UpdateClinicRequest;
        var response = new UpdateClinicResponse(request.CorrelationId);

        var clinicToUpdate = await _unitOfWork
            .Repository<Clinic>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (clinicToUpdate is null)
            throw new NotFoundException(nameof(clinicToUpdate), request.Id);

        clinicToUpdate.UpdateAddress(request.Address);
        clinicToUpdate.UpdateEmailAddress(request.EmailAddress);
        clinicToUpdate.UpdateName(request.Name);
        clinicToUpdate.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Clinic>()
            .UpdateAsync(clinicToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Clinic = _mapper.Map<ClinicDto>(clinicToUpdate);

        await SendIntegrationEventAsync(
            clinicToUpdate,
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
        var message = new ClinicUpdatedIntegrationEventContract
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
            new ClinicUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}