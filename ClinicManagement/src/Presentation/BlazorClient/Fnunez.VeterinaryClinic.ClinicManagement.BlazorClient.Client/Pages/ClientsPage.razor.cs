using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages;

public partial class ClientsPage
{
    private List<ClientDto> Clients = new();
    private ClientDto ToSave = new();
    [Inject]
    IJSRuntime? JSRuntime { get; set; }
    [Inject]
    ClientService? ClientService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ReloadData();
    }

    private void CreateClick()
    {
        if (Clients.Count == 0 || Clients[Clients.Count - 1].ClientId != 0)
        {
            ToSave = new ClientDto();
            Clients.Add(ToSave);
        }
    }

    private void EditClick(int id)
    {
        var client = Clients.Find(x => x.ClientId == id);

        if (client is null)
            throw new ArgumentNullException(nameof(client));

        ToSave = client;
    }

    private async Task DeleteClick(int id)
    {
        if (ClientService is null)
            return;

        if (JSRuntime is null)
            return;

        bool confirmed = await JSRuntime
            .InvokeAsync<bool>("confirm", "Are you sure?");

        if (confirmed)
        {
            await ClientService.DeleteAsync(id);
            await ReloadData();
        }
    }

    private async Task SaveClick()
    {
        if (ClientService is null)
            return;

        if (ToSave.ClientId == 0)
        {
            var toCreate = new CreateClientRequest()
            {
                FullName = ToSave.FullName,
                EmailAddress = ToSave.EmailAddress,
                Salutation = ToSave.Salutation,
                PreferredDoctorId = ToSave.PreferredDoctorId,
                PreferredName = ToSave.PreferredName,
            };
            await ClientService.CreateAsync(toCreate);
        }
        else
        {
            var toUpdate = new UpdateClientRequest()
            {
                ClientId = ToSave.ClientId,
                FullName = ToSave.FullName,
                EmailAddress = ToSave.EmailAddress,
                Salutation = ToSave.Salutation,
                PreferredDoctorId = ToSave.PreferredDoctorId,
                PreferredName = ToSave.PreferredName,
            };

            await ClientService.EditAsync(toUpdate);
        }

        CancelClick();
        await ReloadData();
    }

    private void CancelClick()
    {
        if (ToSave.ClientId == 0)
            Clients.RemoveAt(Clients.Count - 1);

        ToSave = new ClientDto();
    }

    private bool IsAddOrEdit(int id)
    {
        return ToSave.ClientId == id;
    }

    private async Task ReloadData()
    {
        if (ClientService is null)
            return;

        Clients = await ClientService.ListAsync();
    }
}