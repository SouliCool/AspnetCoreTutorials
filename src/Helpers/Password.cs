using System;
using CryptoHelper;

namespace SouliCool.Tutorials.Helpers
{
    public class Password
    {
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }
    }
}
