using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientsFilterClient;

public class ClientFilterValuesSpecification
    : BaseSpecification<Client, ClientFilterValueDto>
{
    public ClientFilterValuesSpecification(
        GetPatientsFilterClientRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);

        Query.Select(c =>
            new ClientFilterValueDto
            {
                EmailAddress = c.EmailAddress,
                FullName = c.FullName,
                Id = c.Id
            }
        );
    }

    private void ApplyFilterSearch(GetPatientsFilterClientRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (string.IsNullOrEmpty(dataGridRequest.Search))
            return;

        string searchFilterValue = dataGridRequest.Search.Trim().ToLower();

        Query
            .Search(c => c.EmailAddress, $"%{searchFilterValue}%")
            .Search(c => c.FullName, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetPatientsFilterClientRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetPatientsFilterClientRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Client> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "EmailAddress":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.EmailAddress);
                else
                    return Query.OrderByDescending(c => c.EmailAddress);

            case "FullName":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.FullName);
                else
                    return Query.OrderByDescending(c => c.FullName);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Client> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "EmailAddress":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.EmailAddress);
                    else
                        orderedBy.ThenByDescending(c => c.EmailAddress);
                    break;

                case "FullName":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.FullName);
                    else
                        orderedBy.ThenByDescending(c => c.FullName);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}