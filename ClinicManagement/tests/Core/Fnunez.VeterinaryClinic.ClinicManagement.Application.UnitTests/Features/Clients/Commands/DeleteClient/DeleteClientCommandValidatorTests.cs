using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandValidatorTests
{
    private readonly DeleteClientCommandValidator _validator;

    public DeleteClientCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new DeleteClientRequest { Id = id };

        var command = new DeleteClientCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeleteClientRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessOrEqualsThanZero_Fails(int id)
    {
        // Arrange
        var request = new DeleteClientRequest { Id = id };

        var command = new DeleteClientCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteClientRequest.Id);
    }
}