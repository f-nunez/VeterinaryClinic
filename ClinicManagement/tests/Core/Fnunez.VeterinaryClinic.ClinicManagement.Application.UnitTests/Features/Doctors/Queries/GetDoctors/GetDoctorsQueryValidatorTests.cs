using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Doctors.Queries.GetDoctors;

public class GetDoctorsQueryValidatorTests
{
    private readonly GetDoctorsQueryValidator _validator;

    public GetDoctorsQueryValidatorTests()
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

        var request = new GetDoctorsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetDoctorsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetDoctorsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsLessThanZero_Fails()
    {
        // Arrange
        int take = -1;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetDoctorsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_FullNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            fullNameFilterValue += "a";

        var request = new GetDoctorsRequest
        {
            FullNameFilterValue = fullNameFilterValue
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.FullNameFilterValue);
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetDoctorsRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsRequest.IdFilterValue);
    }
}