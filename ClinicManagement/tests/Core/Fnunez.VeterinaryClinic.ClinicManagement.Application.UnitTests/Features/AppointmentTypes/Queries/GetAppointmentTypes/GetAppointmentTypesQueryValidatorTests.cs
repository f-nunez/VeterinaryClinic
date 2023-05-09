using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class GetAppointmentTypesQueryValidatorTests
{
    private readonly GetAppointmentTypesQueryValidator _validator;

    public GetAppointmentTypesQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_CodeFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var codeFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            codeFilterValue += "a";

        var request = new GetAppointmentTypesRequest
        {
            CodeFilterValue = codeFilterValue
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.CodeFilterValue);
    }

    [Fact]
    public void Validation_DataGridRequestSearchHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var search = string.Empty;

        for (int i = 0; i < 201; i++)
            search += "a";

        var dataGridRequest = new DataGridRequest { Search = search };

        var request = new GetAppointmentTypesRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetAppointmentTypesRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetAppointmentTypesRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsLessThanZero_Fails()
    {
        // Arrange
        int take = -1;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetAppointmentTypesRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DurationFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var durationFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            durationFilterValue += "a";

        var request = new GetAppointmentTypesRequest
        {
            DurationFilterValue = durationFilterValue
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.DurationFilterValue);
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetAppointmentTypesRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.IdFilterValue);
    }

    [Fact]
    public void Validation_NameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var nameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            nameFilterValue += "a";

        var request = new GetAppointmentTypesRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetAppointmentTypesQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesRequest.NameFilterValue);
    }
}