using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;

public class AppointmentTypeFilterValuesSpecification
    : BaseSpecification<AppointmentType, AppointmentTypeFilterValueDto>
{
    public AppointmentTypeFilterValuesSpecification(
        GetAppointmentsFilterAppointmentTypeRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);

        Query.Select(at =>
            new AppointmentTypeFilterValueDto
            {
                Code = at.Code,
                Duration = at.Duration,
                Id = at.Id,
                Name = at.Name
            }
        );
    }

    private void ApplyFilterSearch(GetAppointmentsFilterAppointmentTypeRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (string.IsNullOrEmpty(dataGridRequest.Search))
            return;

        string searchFilterValue = dataGridRequest.Search.Trim().ToLower();

        Query
            .Search(at => at.Code, $"%{searchFilterValue}%")
            .Search(at => at.Duration.ToString(), $"%{searchFilterValue}%")
            .Search(at => at.Name, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetAppointmentsFilterAppointmentTypeRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetAppointmentsFilterAppointmentTypeRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<AppointmentType> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "Code":
                if (sort.IsAscending)
                    return Query.OrderBy(at => at.Code);
                else
                    return Query.OrderByDescending(at => at.Code);

            case "Duration":
                if (sort.IsAscending)
                    return Query.OrderBy(at => at.Duration);
                else
                    return Query.OrderByDescending(at => at.Duration);

            case "Name":
                if (sort.IsAscending)
                    return Query.OrderBy(at => at.Name);
                else
                    return Query.OrderByDescending(at => at.Name);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<AppointmentType> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "Code":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(at => at.Code);
                    else
                        orderedBy.ThenByDescending(at => at.Code);
                    break;

                case "Duration":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(at => at.Duration);
                    else
                        orderedBy.ThenByDescending(at => at.Duration);
                    break;

                case "Name":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(at => at.Name);
                    else
                        orderedBy.ThenByDescending(at => at.Name);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}