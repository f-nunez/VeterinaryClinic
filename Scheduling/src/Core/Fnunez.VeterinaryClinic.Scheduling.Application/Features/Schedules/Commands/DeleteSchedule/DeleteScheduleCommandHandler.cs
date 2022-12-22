using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.DeleteSchedule;

public class DeleteScheduleCommandHandler
    : IRequestHandler<DeleteScheduleCommand, DeleteScheduleResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteScheduleResponse> Handle(
        DeleteScheduleCommand command,
        CancellationToken cancellationToken)
    {
        DeleteScheduleRequest request = command.DeleteScheduleRequest;
        var response = new DeleteScheduleResponse(request.CorrelationId);
        var scheduleToDelete = _mapper.Map<Schedule>(request);

        await _unitOfWork
            .Repository<Schedule>()
            .DeleteAsync(scheduleToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
