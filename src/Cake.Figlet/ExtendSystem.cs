using System;
using System.Diagnostics;
using System.IO;

namespace Cake.Figlet
{
    internal static class ExtendSystem
    {
        public static Stream GetResourceStream(this object obj, string resourceName)
        {
            var assem = obj.GetType().Assembly;
            return assem.GetManifestResourceStream(resourceName);
        }

        public static int GetIntValue(this string[] arrayStrings, int posi)
        {
            var val = 0;
            if (arrayStrings.Length > posi)
            {
                int.TryParse(arrayStrings[posi], out val);
            }
            return val;
        }        
    }
}