using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdQueryValidator
    : AbstractValidator<GetAppointmentTypesFilterIdQuery>
{
    public GetAppointmentTypesFilterIdQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.")
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");
    }
}