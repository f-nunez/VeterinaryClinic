using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinics;

public class GetClinicsQueryValidator : AbstractValidator<GetClinicsQuery>
{
    public GetClinicsQueryValidator()
    {
        RuleFor(v => v.GetClinicsRequest.AddressFilterValue)
            .MaximumLength(200).WithMessage("AddressFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClinicsRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetClinicsRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetClinicsRequest.DataGridRequest.Take)
            .GreaterThanOrEqualTo(0).WithMessage("Take must be greater than or equal to 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");

        RuleFor(v => v.GetClinicsRequest.EmailAddressFilterValue)
            .MaximumLength(200).WithMessage("EmailAddressFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClinicsRequest.IdFilterValue)
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClinicsRequest.NameFilterValue)
            .MaximumLength(200).WithMessage("NameFilterValue must not exceed 200 characters.");
    }
}