using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace DataService.Api.Security;

public sealed class KeycloakClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return Task.FromResult(principal);
        }

        if (!identity.HasClaim(claim => claim.Type == ClaimTypes.Name) && identity.HasClaim(claim => claim.Type == "preferred_username"))
        {
            var nameClaim = identity.FindFirst("preferred_username");
            if (nameClaim is not null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim.Value));
            }
        }

        var realmAccess = identity.FindFirst("realm_access")?.Value;
        if (!string.IsNullOrWhiteSpace(realmAccess))
        {
            using var document = JsonDocument.Parse(realmAccess);
            if (document.RootElement.TryGetProperty("roles", out var rolesElement))
            {
                foreach (var role in rolesElement.EnumerateArray())
                {
                    var roleValue = role.GetString();
                    if (!string.IsNullOrWhiteSpace(roleValue) && !identity.HasClaim(ClaimTypes.Role, roleValue))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                    }
                }
            }
        }

        var directRoles = identity.FindAll("roles").Select(role => role.Value).ToList();
        foreach (var role in directRoles)
        {
            if (!identity.HasClaim(ClaimTypes.Role, role))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        return Task.FromResult(principal);
    }
}
