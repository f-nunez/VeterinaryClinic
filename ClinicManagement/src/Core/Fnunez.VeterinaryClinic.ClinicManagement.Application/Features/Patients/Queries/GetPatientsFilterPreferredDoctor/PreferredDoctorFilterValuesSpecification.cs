using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

public class PreferredDoctorFilterValuesSpecification
    : BaseSpecification<Doctor, PreferredDoctorFilterValueDto>
{
    public PreferredDoctorFilterValuesSpecification(
        GetPatientsFilterPreferredDoctorRequest request)
    {
        Query
            .AsNoTracking()
            .Where(d => d.IsActive);

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);

        Query.Select(d =>
            new PreferredDoctorFilterValueDto
            {
                FullName = d.FullName,
                Id = d.Id
            }
        );
    }

    private void ApplyFilterSearch(GetPatientsFilterPreferredDoctorRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (string.IsNullOrEmpty(dataGridRequest.Search))
            return;

        string searchFilterValue = dataGridRequest.Search.Trim().ToLower();

        Query.Search(d => d.FullName, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetPatientsFilterPreferredDoctorRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetPatientsFilterPreferredDoctorRequest request)
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

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}