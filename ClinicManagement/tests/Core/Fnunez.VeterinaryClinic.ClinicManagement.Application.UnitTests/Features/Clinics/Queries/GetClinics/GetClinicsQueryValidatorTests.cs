using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Queries.GetClinics;

public class GetClinicsQueryValidatorTests
{
    private readonly GetClinicsQueryValidator _validator;

    public GetClinicsQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AddressFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var addressFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            addressFilterValue += "a";

        var request = new GetClinicsRequest
        {
            AddressFilterValue = addressFilterValue
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.AddressFilterValue);
    }

    [Fact]
    public void Validation_DataGridRequestSearchHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var search = string.Empty;

        for (int i = 0; i < 201; i++)
            search += "a";

        var dataGridRequest = new DataGridRequest { Search = search };

        var request = new GetClinicsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetClinicsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetClinicsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsLessThanZero_Fails()
    {
        // Arrange
        int take = -1;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetClinicsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_EmailAddressFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var emailAddressFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            emailAddressFilterValue += "a";

        var request = new GetClinicsRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.EmailAddressFilterValue);
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetClinicsRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.IdFilterValue);
    }

    [Fact]
    public void Validation_NameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var nameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            nameFilterValue += "a";

        var request = new GetClinicsRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetClinicsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsRequest.NameFilterValue);
    }
}