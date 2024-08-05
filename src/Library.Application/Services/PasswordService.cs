using Library.Application.Services;
using System;
using System.Security.Cryptography;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const int Iterations = 10000;

    public string HashPassword(string password)
    {
        var salt = new byte[SaltSize];

        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var hash = PBKDF2(password, salt, Iterations, HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var hashBytes = Convert.FromBase64String(storedHash);

        var salt = new byte[SaltSize];
        var hash = new byte[HashSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        Array.Copy(hashBytes, SaltSize, hash, 0, HashSize);

        var testHash = PBKDF2(password, salt, Iterations, HashSize);

        for (int i = 0; i < HashSize; i++)
        {
            if (hash[i] != testHash[i])
            {
                return false;
            }
        }

        return true;
    }

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int hashSize)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA1))
        {
            return pbkdf2.GetBytes(hashSize);
        }
    }
}
