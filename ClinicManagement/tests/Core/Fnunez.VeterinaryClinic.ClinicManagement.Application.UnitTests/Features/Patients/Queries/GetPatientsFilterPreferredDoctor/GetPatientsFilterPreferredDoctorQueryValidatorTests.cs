using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorQueryValidatorTests
{
    private readonly GetPatientsFilterPreferredDoctorQueryValidator _validator;

    public GetPatientsFilterPreferredDoctorQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_DataGridRequestSearchHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var search = string.Empty;

        for (int i = 0; i < 201; i++)
            search += "a";

        var dataGridRequest = new DataGridRequest { Search = search };

        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsLessThanZero_Fails()
    {
        // Arrange
        int take = -1;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Take);
    }
}