using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQueryValidatorTests
{
    private readonly GetPatientDetailQueryValidator _validator;

    public GetPatientDetailQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetPatientDetailRequest { ClientId = clientId };

        var query = new GetPatientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetPatientDetailRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange=
        var request = new GetPatientDetailRequest { ClientId = clientId };

        var query = new GetPatientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientDetailRequest.ClientId);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new GetPatientDetailRequest { PatientId = patientId };

        var query = new GetPatientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetPatientDetailRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessThanOrEqualToZero_Fails(int patientId)
    {
        // Arrange=
        var request = new GetPatientDetailRequest { PatientId = patientId };

        var query = new GetPatientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientDetailRequest.PatientId);
    }
}