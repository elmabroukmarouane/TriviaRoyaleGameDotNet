using Blazored.LocalStorage;
using System.Text;
using System.Text.Json;
using TriviaRoyaleGame.Client.Business.Services.CryptoService.Interface;
using TriviaRoyaleGame.Client.Domain.Models;

namespace TriviaRoyaleGame.Client.Business.Extensions.LocalStorage;
public static class LocalStorageManagementExtension
{
    public static async Task SetItemEncryptedAsync<TEntityModelView>(this ILocalStorageService localStorageService, string key, TEntityModelView entity, string? passwordEncyption = null, string? uri = null/*, string? token = null*/, ICryptoService? cryptoService = null)
    {
        var entityJson = JsonSerializer.Serialize(entity);
        var entityJsonBase64 = string.Empty;
        if (!string.IsNullOrWhiteSpace(passwordEncyption))
        {
            entityJsonBase64 = await cryptoService!.EncryptAsync(new CryptoPayloadViewModel
            {
                CryptoSecret = passwordEncyption,
                CryptoText = entityJson
            }, uri/*, token*/); //Helper.EncryptAsync(entityJson, passwordEncyption);
        }
        else
        {
            var entityJsonByte = Encoding.UTF8.GetBytes(entityJson);
            entityJsonBase64 = Convert.ToBase64String(entityJsonByte);
        }
        await localStorageService.SetItemAsync(key, entityJsonBase64);
    }

    public static async Task<TEntityModelView?> GetItemDecryptedAsync<TEntityModelView>(this ILocalStorageService localStorageService, string key, string? passwordEncyption = null, string? uri = null/*, string? token = null*/, ICryptoService? cryptoService = null)
    {
        if (key == null) return default;
        var entityJsonBase64 = await localStorageService.GetItemAsync<string>(key);
        if (entityJsonBase64 == null) return default;
        byte[]? entityJsonByte = null;
        var entityJson = string.Empty;
        if (!string.IsNullOrWhiteSpace(passwordEncyption))
        {
            entityJson = await cryptoService!.DecryptAsync(new CryptoPayloadViewModel
            {
                CryptoSecret = passwordEncyption,
                CryptoText = entityJsonBase64
            }, uri/*, token*/); //await Helper.DecryptAsync(entityJsonBase64, passwordEncyption);
        }
        else
        {
            entityJsonByte = Convert.FromBase64String(entityJsonBase64);
            entityJson = Encoding.UTF8.GetString(entityJsonByte);
        }

        if(entityJson == null) return default;
        entityJson = JsonSerializer.Deserialize<string>(entityJson);
        if (entityJson == null) return default;
        var entity = JsonSerializer.Deserialize<TEntityModelView>(entityJson);
        return entity;
    }
}
