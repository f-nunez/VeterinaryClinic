using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler
    : IRequestHandler<UpdateDoctorCommand, UpdateDoctorResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDoctorCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateDoctorResponse> Handle(
        UpdateDoctorCommand command,
        CancellationToken cancellationToken)
    {
        UpdateDoctorRequest request = command.UpdateDoctorRequest;
        var response = new UpdateDoctorResponse(request.CorrelationId);

        var doctorToUpdate = await _unitOfWork
            .Repository<Doctor>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (doctorToUpdate is null)
            throw new NotFoundException(nameof(doctorToUpdate), request.Id);

        await UpdateDoctorAsync(request, doctorToUpdate, cancellationToken);

        response.Doctor = _mapper.Map<DoctorDto>(doctorToUpdate);

        await SendContractsToServiceBusAsync(
            doctorToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task UpdateDoctorAsync(
        UpdateDoctorRequest request,
        Doctor doctor,
        CancellationToken cancellationToken)
    {
        doctor.UpdateFullName(request.FullName);
        doctor.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Doctor>()
            .UpdateAsync(doctor, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task SendContractsToServiceBusAsync(
        Doctor doctor,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            doctor,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            doctor,
            correlationId,
            cancellationToken
        );
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

    private async Task SendNotificationRequestAsync(
        Doctor doctor,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new DoctorUpdatedNotificationRequestFactory(
            doctor,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}