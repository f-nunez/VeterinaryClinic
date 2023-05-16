using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Clinics.Queries.GetClinicById;

public class GetClinicByIdQueryValidatorTests
{
    private readonly GetClinicByIdQueryValidator _validator;

    public GetClinicByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new GetClinicByIdRequest { Id = id };

        var query = new GetClinicByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetClinicByIdRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new GetClinicByIdRequest { Id = id };

        var query = new GetClinicByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicByIdRequest.Id);
    }
}