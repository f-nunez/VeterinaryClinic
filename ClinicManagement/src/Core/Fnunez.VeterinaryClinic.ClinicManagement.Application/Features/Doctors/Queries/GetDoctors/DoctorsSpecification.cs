using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;

public class DoctorsSpecification : BaseSpecification<Doctor>
{
    public DoctorsSpecification(GetDoctorsRequest request)
    {
        Query
            .AsNoTracking()
            .Where(d => d.IsActive);

        ApplyFilterFullName(request);

        ApplyFilterId(request);

        ApplyOrder(request);

        ApplySearch(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterFullName(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.FullNameFilterValue))
            return;

        string fullNameFilterValue = request.FullNameFilterValue
            .Trim().ToLower();

        Query
            .Where(d => d.FullName.Trim().ToLower().Contains(
                fullNameFilterValue));
    }

    private void ApplyFilterId(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(d => d.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyOrder(GetDoctorsRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySearch(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.DataGridRequest.Search))
            return;

        string search = request.DataGridRequest.Search.Trim().ToLower();

        if (string.IsNullOrEmpty(search))
            return;

        Query
            .Search(d => d.Id.ToString(), $"%{search}%")
            .Search(d => d.FullName, $"%{search}%");
    }

    private void ApplySkipAndTake(GetDoctorsRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Doctor> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "FullName":
                if (sort.IsAscending)
                    return Query.OrderBy(d => d.FullName);
                else
                    return Query.OrderByDescending(d => d.FullName);

            case "Id":
                if (sort.IsAscending)
                    return Query.OrderBy(d => d.Id);
                else
                    return Query.OrderByDescending(d => d.Id);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Doctor> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "FullName":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(d => d.FullName);
                    else
                        orderedBy.ThenByDescending(d => d.FullName);
                    break;

                case "Id":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(d => d.Id);
                    else
                        orderedBy.ThenByDescending(d => d.Id);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}