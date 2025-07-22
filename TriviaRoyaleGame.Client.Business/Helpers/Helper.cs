using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TriviaRoyaleGame.Client.Business.Providers.Classes;
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

namespace TriviaRoyaleGame.Client.Business.Helpers;
public static class Helper
{
    private static readonly byte[] IV =
    {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

    public static readonly string PasswordEncryption = "sf7kL4AjD84bl4UF";

    private static byte[] DeriveKeyFromPassword(string password)
    {
        var emptySalt = Array.Empty<byte>();
        var iterations = 1000;
        var desiredKeyLength = 16; // 16 bytes equal 128 bits.
        var hashMethod = HashAlgorithmName.SHA384;
        return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                         emptySalt,
                                         iterations,
                                         hashMethod,
                                         desiredKeyLength);
    }

    public static async Task<string> EncryptAsync(string plainText, string passphrase)
    {
        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(passphrase);
        aes.IV = IV;

        using MemoryStream output = new();
        using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

        await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(plainText));
        await cryptoStream.FlushFinalBlockAsync();

        return Convert.ToBase64String(output.ToArray());
    }

    public static async Task<string> DecryptAsync(string encryptedBase64, string passphrase)
    {
        var encrypted = Convert.FromBase64String(encryptedBase64);
        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(passphrase);
        aes.IV = IV;

        using MemoryStream input = new(encrypted);
        using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

        using MemoryStream output = new();
        await cryptoStream.CopyToAsync(output);

        return Encoding.Unicode.GetString(output.ToArray());
    }

    public static string? DecryptToken(string? token)
    {
        if (token == null) return null;
        var handler = new JwtSecurityTokenHandler();
        var tokenResponse = handler.ReadJwtToken(token);
        return tokenResponse.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
    }

    //public static string CreateToken(string email, string? password, JwtAppSettings? jwtAppSettings)
    public static string CreateToken(UserViewModel userViewModel, JwtAppSettings? jwtAppSettings)
    {
        //var claims = new[] {
        //        new Claim(JwtRegisteredClaimNames.Email, email),
        //        new Claim(JwtRegisteredClaimNames.Prn, password ?? string.Empty)
        //    };
        var claims = new[] {
            new Claim("user", JsonSerializer.Serialize(userViewModel))
        };
        //var key = new SymmetricSecurityKey(
        //    Encoding.UTF8.GetBytes(JwtAppSettings.Key));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettings!.Key!));
        //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //var token = new JwtSecurityToken(
        //    issuer: JwtAppSettings.Issuer,
        //    audience: JwtAppSettings.Audience,
        //    claims: claims,
        //    expires: DateTime.Now.AddYears(JwtAppSettings.JwtTokenValidite),
        //    signingCredentials: creds);
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
        var userLoggedString = DecryptToken(token);
        var userLogged = JsonSerializer.Deserialize<UserViewModel>(userLoggedString!);
        return userLogged;
    }

    public static async Task<bool> GetAuthentificationStatus(AuthenticationStateProvider? AuthenticationStateProvider)
    {
        if (AuthenticationStateProvider is null) return false;
        var backOfficeAuthenticationStateProvider = (ClientAppAuthenticationStateProvider)AuthenticationStateProvider!;
        var userLogged = await backOfficeAuthenticationStateProvider.GetAuthenticationStateAsync();
        return userLogged != null && userLogged.User.Identity!.IsAuthenticated;
    }
}