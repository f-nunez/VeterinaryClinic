using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandValidatorTests
{
    private readonly DeleteClinicCommandValidator _validator;

    public DeleteClinicCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new DeleteClinicRequest { Id = id };

        var command = new DeleteClinicCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeleteClinicRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessOrEqualsThanZero_Fails(int id)
    {
        // Arrange
        var request = new DeleteClinicRequest { Id = id };

        var command = new DeleteClinicCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteClinicRequest.Id);
    }
}