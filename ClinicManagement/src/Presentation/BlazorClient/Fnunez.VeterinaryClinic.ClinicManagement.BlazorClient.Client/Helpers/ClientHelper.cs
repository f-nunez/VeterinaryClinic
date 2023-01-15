using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class ClientHelper
{
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