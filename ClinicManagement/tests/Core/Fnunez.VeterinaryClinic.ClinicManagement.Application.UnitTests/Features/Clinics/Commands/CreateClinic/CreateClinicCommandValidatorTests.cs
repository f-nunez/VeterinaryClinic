using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandValidatorTests
{
    private readonly CreateClinicCommandValidator _validator;

    public CreateClinicCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1000)]
    public void Validation_AddressHasCharactersBetweenOneAndOneThousand_IsValid(
        int addressLength)
    {
        // Arrange
        var address = string.Empty;

        for (int i = 0; i < addressLength; i++)
            address += "a";

        var request = new CreateClinicRequest { Address = address };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Address);
    }

    [Fact]
    public void Validation_AddressHasMoreThanOneThousandCharacters_Fails()
    {
        // Arrange
        var address = string.Empty;

        for (int i = 0; i < 1001; i++)
            address += "a";

        var request = new CreateClinicRequest { Address = address };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_AddressIsEmpty_Fails(string address)
    {
        // Arrange
        var request = new CreateClinicRequest { Address = address };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Address);
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

        var request = new CreateClinicRequest { EmailAddress = emailAddress };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateClinicRequest.EmailAddress);
    }

    [Fact]
    public void Validation_EmailAddressHasMoreThanThreeHundredAndTwentyCharacters_Fails()
    {
        // Arrange
        var emailEmailAddress = string.Empty;

        for (int i = 0; i < 321; i++)
            emailEmailAddress += "a";

        var request = new CreateClinicRequest { EmailAddress = emailEmailAddress };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.EmailAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_EmailAddressIsEmpty_Fails(string emailEmailAddress)
    {
        // Arrange
        var request = new CreateClinicRequest { EmailAddress = emailEmailAddress };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.EmailAddress);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_NameHasCharactersBetweenOneAndTwoHundred_IsValid(
        int nameLength)
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < nameLength; i++)
            name += "a";

        var request = new CreateClinicRequest { Name = name };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new CreateClinicRequest { Name = name };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(string name)
    {
        // Arrange
        var request = new CreateClinicRequest { Name = name };

        var command = new CreateClinicCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateClinicRequest.Name);
    }
}