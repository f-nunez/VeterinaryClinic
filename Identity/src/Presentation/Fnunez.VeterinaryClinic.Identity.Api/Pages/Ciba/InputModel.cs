// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace IdentityServerHost.Pages.Ciba;

public class InputModel
{
    public string Button { get; set; } = null!;
    public IEnumerable<string> ScopesConsented { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string Description { get; set; } = null!;
}