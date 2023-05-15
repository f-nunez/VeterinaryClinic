using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidatorTests
{
    private readonly UpdatePatientCommandValidator _validator;

    public UpdatePatientCommandValidatorTests()
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

        var request = new UpdatePatientRequest { Breed = breed };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Breed);
    }

    [Fact]
    public void Validation_BreedHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var breed = string.Empty;

        for (int i = 0; i < 201; i++)
            breed += "a";

        var request = new UpdatePatientRequest { Breed = breed };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Breed);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_BreedIsEmpty_Fails(string breed)
    {
        // Arrange
        var request = new UpdatePatientRequest { Breed = breed };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Breed);
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new UpdatePatientRequest { ClientId = clientId };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new UpdatePatientRequest { ClientId = clientId };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.ClientId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(string name)
    {
        // Arrange
        var request = new UpdatePatientRequest { Name = name };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Name);
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

        var request = new UpdatePatientRequest { Name = name };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new UpdatePatientRequest { Name = name };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Name);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new UpdatePatientRequest { PatientId = patientId };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.PatientId);
    }

    [Theory]
    [InlineData(new byte[0])]
    [InlineData(null)]
    public void Validation_PhotoDataIsEmptyAndIsNewPhoto_Fails(byte[] photoData)
    {
        // Arrange
        var isNewPhoto = true;

        var request = new UpdatePatientRequest
        {
            IsNewPhoto = isNewPhoto,
            PhotoData = photoData
        };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.PhotoData);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_PhotoNameIsEmptyAndIsNewPhoto_Fails(string photoName)
    {
        // Arrange
        var isNewPhoto = true;

        var request = new UpdatePatientRequest
        {
            IsNewPhoto = isNewPhoto,
            PhotoName = photoName
        };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.PhotoName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_SpeciesNameIsEmpty_Fails(string species)
    {
        // Arrange
        var request = new UpdatePatientRequest { Species = species };

        var command = new UpdatePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdatePatientRequest.Species);
    }
}