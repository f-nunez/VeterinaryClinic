using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidatorTests
{
    private readonly CreatePatientCommandValidator _validator;

    public CreatePatientCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_BreedHasCharactersBetweenOneAndTwoHundred_IsValid(
        int breedLength)
    {
        // Arrange
        var breed = string.Empty;

        for (int i = 0; i < breedLength; i++)
            breed += "a";

        var request = new CreatePatientRequest { Breed = breed };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Breed);
    }

    [Fact]
    public void Validation_BreedHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var breed = string.Empty;

        for (int i = 0; i < 201; i++)
            breed += "a";

        var request = new CreatePatientRequest { Breed = breed };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Breed);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_BreedIsEmpty_Fails(string breed)
    {
        // Arrange
        var request = new CreatePatientRequest { Breed = breed };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Breed);
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new CreatePatientRequest { ClientId = clientId };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreatePatientRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new CreatePatientRequest { ClientId = clientId };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.ClientId);
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

        var request = new CreatePatientRequest { Name = name };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new CreatePatientRequest { Name = name };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(string name)
    {
        // Arrange
        var request = new CreatePatientRequest { Name = name };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Name);
    }

    [Theory]
    [InlineData(new byte[0])]
    [InlineData(null)]
    public void Validation_PhotoDataIsEmpty_Fails(byte[] photoData)
    {
        // Arrange
        var request = new CreatePatientRequest { PhotoData = photoData };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.PhotoData);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_PhotoNameIsEmpty_Fails(string photoName)
    {
        // Arrange
        var request = new CreatePatientRequest { PhotoName = photoName };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.PhotoName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_SpeciesNameIsEmpty_Fails(string species)
    {
        // Arrange
        var request = new CreatePatientRequest { Species = species };

        var command = new CreatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreatePatientRequest.Species);
    }
}