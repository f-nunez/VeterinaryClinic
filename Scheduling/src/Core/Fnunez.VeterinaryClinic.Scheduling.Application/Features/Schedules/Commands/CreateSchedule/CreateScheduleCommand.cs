using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.CreateSchedule;

public record CreateScheduleCommand(CreateScheduleRequest CreateScheduleRequest)
    : IRequest<CreateScheduleResponse>;