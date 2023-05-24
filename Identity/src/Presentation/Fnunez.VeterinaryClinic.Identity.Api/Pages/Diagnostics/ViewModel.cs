// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Text.Json;

namespace IdentityServerHost.Pages.Diagnostics;

public class ViewModel
{
    public ViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;

        var encoded = result.Properties?.Items["client_list"];

        if (!string.IsNullOrEmpty(encoded))
        {
            var bytes = Base64Url.Decode(encoded);
            var value = Encoding.UTF8.GetString(bytes);

            var deserializedValue = JsonSerializer.Deserialize<string[]>(value);

            if (deserializedValue != null)
                Clients = deserializedValue;
        }
    }

    public AuthenticateResult AuthenticateResult { get; }
    public IEnumerable<string> Clients { get; } = Enumerable.Empty<string>();
}