using System.Security.Cryptography;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users.Errors;
using Zxcvbn;
using Result = DigiDent.Shared.Kernel.ReturnTypes.Result;

namespace DigiDent.UserAccess.Domain.Users.ValueObjects;

/// <summary>
/// Represents a password value object.
/// </summary>
public record Password
{
    private const int Iterations = 20000;
    private const int KeySize = 32;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    public string PasswordHash { get; }
    
    internal Password(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Validates the plain text password and creates a new <see cref="Password"/> instance.
    /// </summary>
    /// <param name="plainTextPassword"> The plain text password to be validated and hashed. </param>
    /// <returns> The <see cref="Result{T}"/> indicating the outcome of the operation. </returns>
    public static Result<Password> Create(string plainTextPassword)
    {
        var validationResult = ValidatePasswordSecurity(plainTextPassword);
        return validationResult.Match(
            onFailure: _ => validationResult.MapToType<Password>(),
            onSuccess: () =>
            {
                var hashedPassword = GetHashedAndSaltedPassword(plainTextPassword);
                return Result.Ok(new Password(hashedPassword));
            });
    }
    
    /// <summary>
    /// Checks if the plain text password is equal to the hashed password.
    /// </summary>
    /// <param name="plainTextPassword"> The plain text password to be checked. </param>
    /// <returns> True if the passwords are equal, false otherwise. </returns>
    public bool IsEqualTo(string plainTextPassword)
    {
        var parts = PasswordHash.Split(':');
        byte[] storedSalt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);
        
        byte[] inputHash = GenerateHash(plainTextPassword, storedSalt);
        
        return CompareByteArrays(storedHash, inputHash);
    }
    
    private static string GetHashedAndSaltedPassword(string plainTextPassword)
    {
        byte[] salt = GenerateSalt();
        byte[] hash = GenerateHash(plainTextPassword, salt);
        
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    private static byte[] GenerateSalt() => 
        RandomNumberGenerator.GetBytes(KeySize);

    private static byte[] GenerateHash(string password, byte[] salt) 
        => Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);

    private static bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (var i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }
        return true;
    }
    
    private static Result ValidatePasswordSecurity(string plainTextPassword)
    {
        const int minLength = 8;
        const int maxLength = 64;
        const int minSecurityLevel = 3;
        
        if (string.IsNullOrWhiteSpace(plainTextPassword) || plainTextPassword.Length < minLength)
            return Result.Fail(PasswordErrors.PasswordIsTooShort(minLength));
        
        if (plainTextPassword.Length > maxLength)
            return Result.Fail(PasswordErrors.PasswordIsTooLong(maxLength));
        
        int securityLevel = Zxcvbn.Core.EvaluatePassword(plainTextPassword).Score;
        
        if (securityLevel < minSecurityLevel)
            return Result.Fail(PasswordErrors.PasswordIsTooWeak);
        
        return Result.Ok();
    }
}