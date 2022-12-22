using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetSchedules;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetSchedules;

public record GetSchedulesQuery(GetSchedulesRequest GetSchedulesRequest)
    : IRequest<GetSchedulesResponse>;