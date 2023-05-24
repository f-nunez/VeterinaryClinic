using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServerHost.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
    public ViewModel? View { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var localIpAddress = HttpContext?.Connection?.LocalIpAddress?.ToString() ?? string.Empty;

        var remoteIpAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();

        var localAddresses = new string[] { "127.0.0.1", "::1", localIpAddress };

        if (!localAddresses.Contains(remoteIpAddress))
            return NotFound();

        if (HttpContext is null)
            return NotFound();

        View = new ViewModel(await HttpContext.AuthenticateAsync());

        return Page();
    }
}