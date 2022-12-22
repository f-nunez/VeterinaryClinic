using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommandHandler
    : IRequestHandler<CreateScheduleCommand, CreateScheduleResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateScheduleResponse> Handle(
        CreateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        CreateScheduleRequest request = command.CreateScheduleRequest;
        var response = new CreateScheduleResponse(request.CorrelationId);
        var newSchedule = MapNewSchedule(request);

        await _unitOfWork
            .Repository<Schedule>()
            .AddAsync(newSchedule, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Schedule = _mapper.Map<ScheduleDto>(newSchedule);

        return response;
    }

    private Schedule MapNewSchedule(CreateScheduleRequest request)
    {
        var dateRange = new DateTimeOffsetRange(
           request.StartOn,
           request.EndOn
       );

        var newSchedule = new Schedule(
            Guid.NewGuid(),
            request.ClinicId,
            dateRange
        );

        return newSchedule;
    }
}
