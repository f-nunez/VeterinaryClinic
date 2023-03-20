using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Doctors;

public partial class DoctorsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IDoctorService _doctorService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditDoctorComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<DoctorDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<DoctorsFilterComponent> _stringLocalizerForFilter { get; set; }

    protected bool CanWrite { get; set; }

    protected RadzenDataGrid<DoctorDto> DoctorsGrid;

    protected List<DoctorDto> Doctors;

    protected int Count;

    [Inject]
    protected IStringLocalizer<DoctorsComponent> StringLocalizer { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;
        var request = new GetDoctorsRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _doctorService
            .DataGridAsync(request);

        Count = dataGridResponse.Count;
        Doctors = dataGridResponse.Items;
        IsLoading = false;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditDoctor>(
            _stringLocalizerForAdd["AddEditDoctor_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsDoctorToAdd", true }
            }
        );

        if (response is null)
            return;

        var savedDoctor = response as DoctorVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Doctors_AddedDoctor_Alert_Message"], savedDoctor.FullName),
            StringLocalizer["Doctors_AddedDoctor_Alert_Title"],
            StringLocalizer["Doctors_AddedDoctor_Alert_Button_Ok"]);

        await DoctorsGrid.Reload();
    }

    protected async Task OnClickDelete(DoctorDto doctor)
    {
        string message = string.Format(
            StringLocalizer["Doctors_DeleteDoctor_Alert_Message"],
            doctor.FullName);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["Doctors_DeleteDoctor_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["Doctors_DeleteDoctor_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["Doctors_DeleteDoctor_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        var request = new DeleteDoctorRequest
        {
            Id = doctor.Id
        };

        await _doctorService.DeleteAsync(request);

        await ShowAlertAsync(
            string.Format(StringLocalizer["Doctors_DeletedDoctor_Alert_Message"], doctor.FullName),
            StringLocalizer["Doctors_DeletedDoctor_Alert_Title"],
            StringLocalizer["Doctors_DeletedDoctor_Alert_Button_Ok"]);

        await DoctorsGrid.ReloadAfterDeleteItemAsync();
    }

    protected async Task OnClickDetail(DoctorDto doctor)
    {
        var request = new GetDoctorByIdRequest
        {
            Id = doctor.Id
        };

        var currentDoctor = await _doctorService.GetByIdAsync(request);

        var doctorForDetail = DoctorHelper.MapDoctorViewModel(doctor);

        await _dialogService.OpenAsync<DoctorDetail>(
            _stringLocalizerForDetail["DoctorDetail_Label_DoctorDetail"],
            new Dictionary<string, object>
            {
                { "Model", doctorForDetail }
            }
        );
    }

    protected async Task OnClickEdit(DoctorDto doctor)
    {
        var request = new GetDoctorByIdRequest
        {
            Id = doctor.Id
        };

        var currentDoctor = await _doctorService.GetByIdAsync(request);

        var doctorToEdit = DoctorHelper.MapDoctorViewModel(doctor);

        var response = await _dialogService.OpenAsync<AddEditDoctor>(
            _stringLocalizerForAdd["AddEditDoctor_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsDoctorToAdd", false },
                { "Model", doctorToEdit }
            }
        );

        if (response is null)
            return;

        var savedDoctor = response as DoctorVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Doctors_EditedDoctor_Alert_Message"], savedDoctor.FullName),
            StringLocalizer["Doctors_EditedDoctor_Alert_Title"],
            StringLocalizer["Doctors_EditedDoctor_Alert_Button_Ok"]);

        await DoctorsGrid.Reload();
    }

    protected async Task OnClickFilterSearch()
    {
        await ResetGridAndSearchAsync();
    }

    protected async Task OnKeyUpFilterSearch(KeyboardEventArgs args)
    {
        if (args.Key != "Enter")
            return;

        await ResetGridAndSearchAsync();
    }

    protected async Task OpenFilterMenu()
    {
        var filterValues = new DoctorsFilterValues
        {
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(DoctorsFilterValues), filterValues }
        };

        var result = await _dialogService.OpenSideAsync<DoctorsFilter>(
            _stringLocalizerForFilter["DoctorsFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(DoctorsFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        FullNameFilterValue = filterValues.FullNameFilterValue;
        IdFilterValue = filterValues.IdFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        DoctorsGrid.Reset(false);
        await DoctorsGrid.FirstPage(true);
    }

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
}