using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.Common.Helpers
{
    public class UtilHelper
    {
        public static bool CompareByte(byte[] userPassword, byte[] inputPassword)
        {
            if (userPassword == inputPassword)
            {
                return true;
            }

            if (userPassword.Length != inputPassword.Length) {
                return false;
            }

            bool passwordMatch = true;
            for (int i = 0; i < inputPassword.Length && i < userPassword.Length; i++)
            {
                if (passwordMatch)
                {
                    if (userPassword[i] != inputPassword[i])
                        passwordMatch = false;
                }
            }

            return passwordMatch;
        }
    }
}