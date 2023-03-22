using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class ClientHelper
{
    public static AddEditClientVm MapAddEditClientViewModel(
        ClientDto clientDto)
    {
        return new AddEditClientVm
        {
            ClientId = clientDto.ClientId,
            EmailAddress = clientDto.EmailAddress,
            FullName = clientDto.FullName,
            PreferredDoctorId = clientDto.PreferredDoctorId,
            PreferredLanguage = clientDto.PreferredLanguage,
            PreferredName = clientDto.PreferredName,
            Salutation = clientDto.Salutation
        };
    }

    public static ClientDetailVm MapClientDetailViewModel(
        ClientDetailDto clientDetailDto)
    {
        return new ClientDetailVm
        {
            ClientId = clientDetailDto.ClientId,
            EmailAddress = clientDetailDto.EmailAddress,
            FullName = clientDetailDto.FullName,
            IsActive = clientDetailDto.IsActive,
            PreferredDoctorFullName = clientDetailDto.PreferredDoctorFullName,
            PreferredName = clientDetailDto.PreferredName,
            Salutation = clientDetailDto.Salutation
        };
    }

    public static ClientVm MapClientViewModel(ClientDto clientDto)
    {
        return new ClientVm
        {
            ClientId = clientDto.ClientId,
            EmailAddress = clientDto.EmailAddress,
            FullName = clientDto.FullName,
            PreferredDoctorId = clientDto.PreferredDoctorId,
            PreferredName = clientDto.PreferredName,
            Salutation = clientDto.Salutation
        };
    }
}