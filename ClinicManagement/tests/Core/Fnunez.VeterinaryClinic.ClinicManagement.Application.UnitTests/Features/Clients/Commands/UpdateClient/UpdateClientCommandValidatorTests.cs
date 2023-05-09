using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandValidatorTests
{
    private readonly UpdateClientCommandValidator _validator;

    public UpdateClientCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(320)]
    public void Validation_EmailAddressHasCharactersBetweenOneAndThreeHundredAndTwenty_IsValid(
        int emailAddressLength)
    {
        // Arrange
        var emailAddress = string.Empty;

        for (int i = 0; i < emailAddressLength; i++)
            emailAddress += "a";

        var request = new UpdateClientRequest { EmailAddress = emailAddress };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateClientRequest.EmailAddress);
    }

    [Fact]
    public void Validation_EmailAddressHasMoreThanThreeHundredAndTwentyCharacters_Fails()
    {
        // Arrange
        var emailAddress = string.Empty;

        for (int i = 0; i < 321; i++)
            emailAddress += "a";

        var request = new UpdateClientRequest { EmailAddress = emailAddress };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.EmailAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_EmailAddressIsEmpty_Fails(string emailAddress)
    {
        // Arrange
        var request = new UpdateClientRequest { EmailAddress = emailAddress };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.EmailAddress);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_FullNameHasCharactersBetweenOneAndTwoHundred_IsValid(
        int fullNameLength)
    {
        // Arrange
        var fullName = string.Empty;

        for (int i = 0; i < fullNameLength; i++)
            fullName += "a";

        var request = new UpdateClientRequest { FullName = fullName };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateClientRequest.FullName);
    }

    [Fact]
    public void Validation_FullNameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullName = string.Empty;

        for (int i = 0; i < 201; i++)
            fullName += "a";

        var request = new UpdateClientRequest { FullName = fullName };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_FullNameIsEmpty_Fails(string fullName)
    {
        // Arrange
        var request = new UpdateClientRequest { FullName = fullName };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.FullName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_PreferredNameHasCharactersBetweenOneAndTwoHundred_IsValid(
        int preferredNameLength)
    {
        // Arrange
        var preferredName = string.Empty;

        for (int i = 0; i < preferredNameLength; i++)
            preferredName += "a";

        var request = new UpdateClientRequest { PreferredName = preferredName };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateClientRequest.PreferredName);
    }

    [Fact]
    public void Validation_PreferredNameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var preferredName = string.Empty;

        for (int i = 0; i < 201; i++)
            preferredName += "a";

        var request = new UpdateClientRequest { PreferredName = preferredName };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.PreferredName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_PreferredNameIsEmpty_Fails(string preferredName)
    {
        // Arrange
        var request = new UpdateClientRequest
        {
            PreferredName = preferredName
        };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.PreferredName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_SalutationHasCharactersBetweenOneAndTwoHundred_IsValid(
        int salutationLength)
    {
        // Arrange
        var salutation = string.Empty;

        for (int i = 0; i < salutationLength; i++)
            salutation += "a";

        var request = new UpdateClientRequest { Salutation = salutation };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateClientRequest.Salutation);
    }

    [Fact]
    public void Validation_SalutationHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var salutation = string.Empty;

        for (int i = 0; i < 201; i++)
            salutation += "a";

        var request = new UpdateClientRequest { Salutation = salutation };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.Salutation);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_SalutationIsEmpty_Fails(string salutation)
    {
        // Arrange
        var request = new UpdateClientRequest { Salutation = salutation };

        var command = new UpdateClientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateClientRequest.Salutation);
    }
}