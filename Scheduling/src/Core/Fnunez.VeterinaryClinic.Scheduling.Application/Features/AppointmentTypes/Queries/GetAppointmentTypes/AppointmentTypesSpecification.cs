using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class AppointmentTypesSpecification : BaseSpecification<AppointmentType>
{
    public AppointmentTypesSpecification(GetAppointmentTypesRequest request)
    {
        Query
            .AsNoTracking()
            .Where(at => at.IsActive);

        ApplyFilterCode(request);

        ApplyFilterDuration(request);

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyOrder(request);

        ApplySearch(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterCode(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.CodeFilterValue))
            return;

        string codeFilterValue = request.CodeFilterValue.Trim().ToLower();

        Query
            .Where(at => at.Code.Trim().ToLower().Contains(codeFilterValue));
    }

    private void ApplyFilterDuration(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.DurationFilterValue))
            return;

        string durationFilterValue = request.DurationFilterValue
            .Trim().ToLower();

        Query
            .Where(at => at.Duration.ToString().Contains(durationFilterValue));
    }

    private void ApplyFilterId(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(at => at.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(at => at.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyOrder(GetAppointmentTypesRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySearch(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.DataGridRequest.Search))
            return;

        string search = request.DataGridRequest.Search.Trim().ToLower();

        if (string.IsNullOrEmpty(search))
            return;

        Query
            .Search(at => at.Code, $"%{search}%")
            .Search(at => at.Duration.ToString(), $"%{search}%")
            .Search(at => at.Id.ToString(), $"%{search}%")
            .Search(at => at.Name, $"%{search}%");
    }

    private void ApplySkipAndTake(GetAppointmentTypesRequest request)
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

            case "Id":
                if (sort.IsAscending)
                    return Query.OrderBy(at => at.Id);
                else
                    return Query.OrderByDescending(at => at.Id);

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

                case "Id":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(at => at.Id);
                    else
                        orderedBy.ThenByDescending(at => at.Id);
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