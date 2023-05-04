using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Unauthorized;

public partial class UnauthorizedComponent : ComponentBase
{
    [Inject]
    protected IStringLocalizer<UnauthorizedComponent> StringLocalizer { get; set; }
}