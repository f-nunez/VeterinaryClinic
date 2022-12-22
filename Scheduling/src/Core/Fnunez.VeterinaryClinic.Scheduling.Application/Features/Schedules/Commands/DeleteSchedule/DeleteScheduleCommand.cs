using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.DeleteSchedule;

public record DeleteScheduleCommand(DeleteScheduleRequest DeleteScheduleRequest)
    : IRequest<DeleteScheduleResponse>;