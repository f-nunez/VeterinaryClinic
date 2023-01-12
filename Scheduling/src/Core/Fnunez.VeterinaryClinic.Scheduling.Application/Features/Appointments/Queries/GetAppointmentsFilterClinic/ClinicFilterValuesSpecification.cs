using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClinic;

public class ClinicFilterValuesSpecification
    : BaseSpecification<Clinic, ClinicFilterValueDto>
{
    public ClinicFilterValuesSpecification(
        GetAppointmentsFilterClinicRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);

        Query.Select(d =>
            new ClinicFilterValueDto
            {
                Address = d.Address,
                Id = d.Id,
                Name = d.Name
            }
        );
    }

    private void ApplyFilterSearch(GetAppointmentsFilterClinicRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (string.IsNullOrEmpty(dataGridRequest.Search))
            return;

        string searchFilterValue = dataGridRequest.Search.Trim().ToLower();

        Query
            .Search(d => d.Address, $"%{searchFilterValue}%")
            .Search(d => d.Name, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetAppointmentsFilterClinicRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetAppointmentsFilterClinicRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Clinic> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "Address":
                if (sort.IsAscending)
                    return Query.OrderBy(d => d.Address);
                else
                    return Query.OrderByDescending(d => d.Address);

            case "Name":
                if (sort.IsAscending)
                    return Query.OrderBy(d => d.Name);
                else
                    return Query.OrderByDescending(d => d.Name);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Clinic> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "Address":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(d => d.Address);
                    else
                        orderedBy.ThenByDescending(d => d.Address);
                    break;

                case "Name":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(d => d.Name);
                    else
                        orderedBy.ThenByDescending(d => d.Name);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}