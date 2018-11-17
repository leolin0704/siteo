using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static List<TTaget> ConvertObjList<TTaget, TSource>(ICollection<TSource> source) where TTaget : new(){
            if (source == null) {
                return null;
            }

            var result = new List<TTaget>();
            List<string> properties = new List<string>();

            PropertyInfo[] props = typeof(TTaget).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in props)
            {
                properties.Add(p.Name);
            }
            foreach (var item in source)
            {
                var target = new TTaget();
                CopyProperties(item, target, properties.ToArray());
                result.Add(target);
            }


            return result;

        }


        public static void CopyProperties(object source, object target, params string[] propertyNames)
        {
            if (source == null || target == null || propertyNames == null || propertyNames.Length == 0) {
                return;
            }

            var sType = source.GetType();
            var tType = target.GetType();

            foreach(string name in propertyNames)
            {
                try
                {
                    var sp = sType.GetProperty(name);
                    var tp = tType.GetProperty(name);

                    tp.SetValue(target, sp.GetValue(source), null);
                }
                catch
                {
                    
                }
            }

        }
    }
}