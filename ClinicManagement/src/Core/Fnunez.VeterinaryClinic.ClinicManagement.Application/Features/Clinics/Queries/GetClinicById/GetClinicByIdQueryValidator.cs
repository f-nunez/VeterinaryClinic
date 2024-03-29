using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicById;

public class GetClinicByIdQueryValidator
    : AbstractValidator<GetClinicByIdQuery>
{
    public GetClinicByIdQueryValidator()
    {
        RuleFor(v => v.GetClinicByIdRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}