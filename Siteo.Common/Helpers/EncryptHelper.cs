using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Siteo.Common.Helpers
{
    public static class EncryptHelper
    {
        public static byte[] Encrypt(string password)
        {
            MD5CryptoServiceProvider Hash = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            return Hash.ComputeHash(encoder.GetBytes(password));
        }

        public static string EncryptString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Encrypt(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}