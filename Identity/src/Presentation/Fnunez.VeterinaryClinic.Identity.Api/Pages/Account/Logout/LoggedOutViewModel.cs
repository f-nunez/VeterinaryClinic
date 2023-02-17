
// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace IdentityServerHost.Pages.Logout;

public class LoggedOutViewModel
{
    public string PostLogoutRedirectUri { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string SignOutIframeUrl { get; set; } = null!;
    public bool AutomaticRedirectAfterSignOut { get; set; }
}