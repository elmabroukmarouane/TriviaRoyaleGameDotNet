using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Business.Services.Interfaces;
public interface ICryptoService
{
    Task<string?> EncryptAsync(CryptoPayload cryptoPayload);
    Task<string?> DecryptAsync(CryptoPayload cryptoPayload);
}
