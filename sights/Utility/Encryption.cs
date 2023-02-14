using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace sights.Utility
{
    public static class Encryption
    {
        public static string HashPbkdf2(string password)
        {
            byte[] encrypted = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);


            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encrypted.Length; i++)
            {
                builder.Append(encrypted[i].ToString("x2"));
            }

            string encryptedString = builder.ToString();

            return encryptedString;
        }
    }
}
