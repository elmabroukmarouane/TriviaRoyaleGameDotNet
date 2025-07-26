using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TriviaRoyaleGame.Client.Business.Providers.Classes;
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

namespace TriviaRoyaleGame.Client.Business.Helpers;
public static class Helper
{
    public static string? DecryptToken(string? token, string type)
    {
        if (token == null) return null;
        var handler = new JwtSecurityTokenHandler();
        var tokenResponse = handler.ReadJwtToken(token);
        return tokenResponse.Claims.FirstOrDefault(c => c.Type.ToLower().Contains(type))?.Value;
    }

    public static string CreateToken(UserViewModel userViewModel, JwtAppSettings? jwtAppSettings)
    {
        var claims = new[] {
            new Claim("user", JsonSerializer.Serialize(userViewModel))
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettings!.Key!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            issuer: jwtAppSettings.Issuer,
            audience: jwtAppSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddYears((int)jwtAppSettings!.JwtTokenValidite!),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static UserViewModel? DecryptAndDeserializeUserViewModel(string? token)
    {
        if(token is null) return null;
        var userLoggedString = DecryptToken(token, "user");
        var userLogged = JsonSerializer.Deserialize<UserViewModel>(userLoggedString!);
        return userLogged;
    }

    public static string? GetRoleConnectedUser(string? token)
    {
        if (token is null) return null;
        return DecryptToken(token, "role"); ;
    }

    public static async Task<bool> GetAuthentificationStatus(AuthenticationStateProvider? AuthenticationStateProvider)
    {
        if (AuthenticationStateProvider is null) return false;
        var backOfficeAuthenticationStateProvider = (ClientAppAuthenticationStateProvider)AuthenticationStateProvider!;
        var userLogged = await backOfficeAuthenticationStateProvider.GetAuthenticationStateAsync();
        return userLogged != null && userLogged.User.Identity!.IsAuthenticated;
    }
}