﻿using System.Security.Cryptography;
using System.Text;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Business.Services.Classes;
public class CryptoService : ICryptoService
{
    private readonly byte[] IV =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

    public async Task<string?> DecryptAsync(CryptoPayload cryptoPayload)
    {
        var encrypted = Convert.FromBase64String(cryptoPayload.CryptoText!);
        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(cryptoPayload.CryptoSecret!);
        aes.IV = IV;

        using MemoryStream input = new(encrypted);
        using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

        using MemoryStream output = new();
        await cryptoStream.CopyToAsync(output);

        return Encoding.Unicode.GetString(output.ToArray());
    }

    public async Task<string?> EncryptAsync(CryptoPayload cryptoPayload)
    {
        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(cryptoPayload.CryptoSecret!);
        aes.IV = IV;

        using MemoryStream output = new();
        using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

        await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(cryptoPayload.CryptoText!));
        await cryptoStream.FlushFinalBlockAsync();

        return Convert.ToBase64String(output.ToArray());
    }

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
}