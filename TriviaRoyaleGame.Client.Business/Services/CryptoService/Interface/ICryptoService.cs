using TriviaRoyaleGame.Client.Domain.Models;

namespace TriviaRoyaleGame.Client.Business.Services.CryptoService.Interface;
public interface ICryptoService
{
    Task<string?> EncryptAsync(CryptoPayloadViewModel cryptoPayloadViewModel, string? uri, string? token);
    //Task<string?> DecryptAsync(CryptoPayloadViewModel cryptoPayloadViewModel, string? uri, string? token);
}
