using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandHandler
    : IRequestHandler<DeleteAppointmentCommand, DeleteAppointmentResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentResponse> Handle(
        DeleteAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentRequest request = command.DeleteAppointmentRequest;
        var response = new DeleteAppointmentResponse(request.CorrelationId);

        var appointmentToDelete = await _unitOfWork
            .Repository<Appointment>()
            .GetByIdAsync(request.AppointmentId, cancellationToken);

        if (appointmentToDelete is null)
            throw new NotFoundException(
                nameof(appointmentToDelete), request.AppointmentId);

        appointmentToDelete.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Appointment>()
            .DeleteAsync(appointmentToDelete, cancellationToken);

        await _unitOfWork.CommitAsync();

        return response;
    }
}