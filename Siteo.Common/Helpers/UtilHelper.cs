using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Siteo.Common.Helpers
{
    public class UtilHelper
    {
        public static bool CompareString(string a, string b)
        {
            return string.Compare(a, b) == 0;
        }

        public static bool CompareByte(byte[] a, byte[] b)
        {
            if (a == b)
            {
                return true;
            }

            if (a.Length != b.Length)
            {
                return false;
            }

            bool passwordMatch = true;
            for (int i = 0; i < b.Length && i < a.Length; i++)
            {
                if (passwordMatch)
                {
                    if (a[i] != b[i])
                        passwordMatch = false;
                }
            }

            return passwordMatch;
        }

        public static List<TTaget> ConvertObjList<TSource, TTaget>(ICollection<TSource> source) where TTaget : new()
        {
            var newSource = new List<object>();
            foreach (var item in source)
            {
                newSource.Add(item);
            }
            
            return ConvertObjList<TTaget>(newSource);
        }
        
        public static List<TTaget> ConvertObjList<TTaget>(ICollection<object> source) where TTaget : new()
        {
            if (source == null)
            {
                return null;
            }

            var result = new List<TTaget>();

            foreach (var item in source)
            {
                var target = new TTaget();
                CopyProperties(item, target);
                result.Add(target);
            }


            return result;

        }

        public static void ConvertChildObjList<TSource, TTaget, TChildSource, TChildTarget>(List<TSource> source, List<TTaget> target, string property) where TChildTarget : new()
        {
            ConvertChildObjList<TSource, TTaget, TChildSource, TChildTarget>(source, target, property, property);
        }

        public static void ConvertChildObjList<TSource, TTaget, TChildSource, TChildTarget>(List<TSource> source, List<TTaget> target, string sourceChildProperty, string targetChildProperty) where TChildTarget : new()
        {
            for (int i = 0; i < source.Count; i++)
            {
                var currentModule = source[i];
                var currentModuleModel = target[i];

                var sp = currentModule.GetType().GetProperty(sourceChildProperty);
                var tp = currentModuleModel.GetType().GetProperty(targetChildProperty);

                var childSource = sp.GetValue(currentModule) as ICollection<TChildSource>;

                tp.SetValue(currentModuleModel, ConvertObjList<TChildSource, TChildTarget>(childSource));
            }

        }

        public static void CopyProperties(object source, object target)
        {
            PropertyInfo[] props = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> properties = new List<string>();
            foreach (PropertyInfo p in props)
            {
                properties.Add(p.Name);
            }

            CopyProperties(source, target, properties.ToArray());
        }

        public static TTarget CopyProperties<TTarget>(object source) where TTarget : new()
        {
            var target = new TTarget();
            PropertyInfo[] props = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> properties = new List<string>();
            foreach (PropertyInfo p in props)
            {
                properties.Add(p.Name);
            }

            CopyProperties(source, target, properties.ToArray());

            return target;
        }

        public static void CopyProperties(object source, object target, params string[] propertyNames)
        {
            if (source == null || target == null || propertyNames == null || propertyNames.Length == 0)
            {
                return;
            }

            var sType = source.GetType();
            var tType = target.GetType();

            foreach (string name in propertyNames)
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