using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Patients.Queries.GetPatientsFilterClient;

public class GetPatientsFilterClientQueryValidatorTests
{
    private readonly GetPatientsFilterClientQueryValidator _validator;

    public GetPatientsFilterClientQueryValidatorTests()
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

        var request = new GetPatientsFilterClientRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterClientQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterClientRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetPatientsFilterClientRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterClientQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterClientRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetPatientsFilterClientRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterClientQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterClientRequest.DataGridRequest.Take);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DataGridRequestTakeIsLessThanOrEqualToZero_Fails(
        int take)
    {
        // Arrange
        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetPatientsFilterClientRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetPatientsFilterClientQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsFilterClientRequest.DataGridRequest.Take);
    }
}