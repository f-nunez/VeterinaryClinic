using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Patients;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Patients;

public partial class AddEditPatientComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IPatientService _patientService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected List<AnimalSexDropDownValue> AnimalSexDropDownValues { get; set; }

    protected bool IsSaving { get; set; }

    protected int? Sex { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditPatientComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsPatientToAdd { get; set; }

    [Parameter]
    public AddEditPatientVm Model { get; set; } = new();

    #region PreferredDoctor properties
    protected RadzenDropDownDataGrid<int?> PreferredDoctorDropDownDataGrid;

    protected int PreferredDoctorFilterCount;

    protected List<PreferredDoctorFilterValueDto> PreferredDoctorFilterValues = new();

    [Parameter]
    public List<PreferredDoctorFilterValueDto> PreselectedPreferredDoctorFilterValues { get; set; }
    #endregion

    #region Photo Uploader properties
    protected string PhotoBase64Encoded { get; set; }
    #endregion

    protected override void OnInitialized()
    {
        PhotoBase64Encoded = Model.PhotoData is null
            ? PatientHelper.GetPatientThumbnail()
            : Convert.ToBase64String(Model.PhotoData);

        AnimalSexDropDownValues = new List<AnimalSexDropDownValue>
        {
            new AnimalSexDropDownValue
            {
                Text = StringLocalizer["AddEditPatient_AnimalSex_Female"],
                Value = (int)AnimalSex.Female
            },
            new AnimalSexDropDownValue
            {
                Text = StringLocalizer["AddEditPatient_AnimalSex_Male"],
                Value = (int)AnimalSex.Male
            }
        };
    }

    protected override void OnParametersSet()
    {
        if (!IsPatientToAdd)
        {
            PreferredDoctorFilterValues = PreselectedPreferredDoctorFilterValues;
            Sex = (int)Model.Sex;
        }
    }

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsPatientToAdd)
            await CreatePatientAsync();
        else
            await UpdatePatientAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreatePatientAsync()
    {
        var request = PatientHelper.MapCreatePatientRequest(Model);

        await _patientService.CreateAsync(request);
    }

    private async Task UpdatePatientAsync()
    {
        var request = PatientHelper.MapUpdatePatientRequest(Model);

        await _patientService.UpdateAsync(request);
    }

    #region Animal sex dropdown methods
    protected void OnChangeSexDropDown(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value >= 0)
        {
            Model.Sex = (AnimalSex)Enum
                .ToObject(typeof(AnimalSex), convertedValue.Value);
        }
        else
        {
            Model.Sex = null;
        }
    }
    #endregion

    #region PreferredDoctor filter methods
    protected async Task PreferredDoctorFilterLoadData(LoadDataArgs args)
    {
        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _patientService
            .DataGridFilterPreferredDoctorAsync(request);

        PreferredDoctorFilterValues = dataGridResponse.Items;

        PreferredDoctorFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected void OnChangePreferredDoctorFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            Model.PreferredDoctorId = convertedValue.Value;
        }
        else
        {
            Model.PreferredDoctorId = null;
        }
    }
    #endregion

    #region Photo Uploader methods
    protected async void OnChangePhoto(InputFileChangeEventArgs e)
    {
        Stream photoStream = e.File.OpenReadStream(10485760);

        using (var memoryStream = new MemoryStream())
        {
            await photoStream.CopyToAsync(memoryStream);
            Model.PhotoData = memoryStream.ToArray();
        }

        PhotoBase64Encoded = Convert.ToBase64String(Model.PhotoData);

        Model.PhotoName = e.File.Name;

        Model.IsNewPhoto = true;

        await InvokeAsync(StateHasChanged);
    }
    #endregion
}