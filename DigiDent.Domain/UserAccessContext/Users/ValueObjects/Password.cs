using System.Security.Cryptography;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users.Errors;
using Zxcvbn;
using Result = DigiDent.Domain.SharedKernel.ReturnTypes.Result;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record Password
{
    private const int Iterations = 20000;
    private const int KeySize = 32;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    private const int MinLength = 8;
    private const int MaxLength = 64;
    private const int MinSecurityLevel = 3;
    
    public string PasswordHash { get; }
    
    internal Password(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

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
    
    public bool IsEqualTo(string plainTextPassword)
    {
        var parts = PasswordHash.Split(':');
        byte[] storedSalt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);
        
        byte[] inputHash = GenerateHash(plainTextPassword, storedSalt);
        
        return CompareByteArrays(storedHash, inputHash);
    }
    
    internal static Password TempAdminPassword 
        => Create("*tempAdminPass!").Value!;
    
    private static string GetHashedAndSaltedPassword(string plainTextPassword)
    {
        byte[] salt = GenerateSalt();
        byte[] hash = GenerateHash(plainTextPassword, salt);
        
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    private static byte[] GenerateSalt() => 
        RandomNumberGenerator.GetBytes(KeySize);

    private static byte[] GenerateHash(string password, byte[] salt) 
        => Rfc2898DeriveBytes.Pbkdf2(password, salt,Iterations, HashAlgorithm, KeySize);

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
        if (string.IsNullOrWhiteSpace(plainTextPassword) || plainTextPassword.Length < MinLength)
            return Result.Fail(PasswordErrors.PasswordIsTooShort(MinLength));
        
        if (plainTextPassword.Length > MaxLength)
            return Result.Fail(PasswordErrors.PasswordIsTooLong(MaxLength));
        
        int securityLevel = Core.EvaluatePassword(plainTextPassword).Score;
        if (securityLevel < MinSecurityLevel)
            return Result.Fail(PasswordErrors.PasswordIsTooWeak);
        
        return Result.Ok();
    }
}