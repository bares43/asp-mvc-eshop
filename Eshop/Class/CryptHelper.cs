using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevOne.Security.Cryptography.BCrypt;

namespace Eshop.Class
{
    public class CryptHelper
    {
        public static string Hash(string str)
        {
            return BCryptHelper.HashPassword(str, BCryptHelper.GenerateSalt(12));
        }

        public static bool Verify(string str, string hash)
        {
            return BCryptHelper.CheckPassword(str, hash);
        }
    }
}