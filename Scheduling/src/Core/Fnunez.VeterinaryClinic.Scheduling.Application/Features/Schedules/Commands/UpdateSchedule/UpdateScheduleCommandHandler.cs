using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, UpdateScheduleResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateScheduleResponse> Handle(
        UpdateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        UpdateScheduleRequest request = command.UpdateScheduleRequest;
        var response = new UpdateScheduleResponse(request.CorrelationId);
        var scheduleToUpdate = _mapper.Map<Schedule>(request);

        await _unitOfWork.Repository<Schedule>()
            .UpdateAsync(scheduleToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Schedule = _mapper.Map<ScheduleDto>(scheduleToUpdate);

        return response;
    }
}
