using System.Security.Cryptography;
using System.Text;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record class Password
{
    private const int Iterations = 20000;
    private const int KeySize = 32;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    public string PasswordHash { get; private set; }
    
    private Password(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public static Password Create(string plainTextPassword)
    {
        byte[] salt = GenerateSalt();
        
        byte[] hash = GenerateHash(plainTextPassword, salt);
        
        var hashedPassword = $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";

        return new Password(hashedPassword);
    }

    public bool IsEqualTo(string plainTextPassword)
    {
        var parts = PasswordHash.Split(':');
        byte[] storedSalt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);
        
        byte[] inputHash = GenerateHash(plainTextPassword, storedSalt);
        
        return CompareByteArrays(storedHash, inputHash);
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
}