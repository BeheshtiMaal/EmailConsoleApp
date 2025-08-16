using System.Text;
using System;
using System.Security.Cryptography;

namespace EmailConsoleApp
{
    internal class PasswordHasher
    {
        public static string Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(bytes);

                // Convert bytes to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2")); // "x2" = lowercase hex

                return sb.ToString();
            }
        }
    }

}
