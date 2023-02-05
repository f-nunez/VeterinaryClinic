// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Pages.Login;

public class InputModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public bool RememberLogin { get; set; }

    public string ReturnUrl { get; set; } = null!;

    public string Button { get; set; } = null!;
}