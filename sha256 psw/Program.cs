using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string GenerateHash(string password, string salt)
    {
        using (SHA256Managed crypt = new SHA256Managed())
        {
            // Hash della password
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = crypt.ComputeHash(passwordBytes);
            string hashedPassword = ByteArrayToHexString(passwordHash);

            // Hash del valore di sale
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] saltHash = crypt.ComputeHash(saltBytes);
            string hashedSalt = ByteArrayToHexString(saltHash);

            // Combinazione degli hash
            string combinedHash = hashedSalt + hashedPassword;

            // Hash finale
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(combinedHash));
            string finalHash = ByteArrayToHexString(crypto);

            return finalHash;
        }
    }

    private static string ByteArrayToHexString(byte[] byteArray)
    {
        StringBuilder hexBuilder = new StringBuilder(byteArray.Length * 2);
        foreach (byte b in byteArray)
        {
            hexBuilder.Append(b.ToString("x2"));
        }
        return hexBuilder.ToString();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Inserisci il valore di sale (salt): ");
        string salt = Console.ReadLine();

        Console.Write("Inserisci la password: ");
        string password = Console.ReadLine();

        string hashedPassword = PasswordHasher.GenerateHash(password, salt);

        Console.WriteLine("Hashed Password: " + hashedPassword);
    }
}

