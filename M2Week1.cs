using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Discussions
{
    public static class Class1
    {
        public enum Color
        {
            Unknown = 0,
            Red,
            Blue,
            Green,
            Yellow,
            Black,
            White,
            Brown,
            Orange,
            Purple,
            Magenta,
            OsageOrange,
            BurntUmber
        }
        public static Color ToClor(this string inputColor)
        {
            var inputIsValid = Enum.TryParse(inputColor, true, out Color color);
            // terenary operator
            return inputIsValid ? color : Color.Unknown;
        }
        
        public static string ToClor(this Color color) 
        { 
            return color.ToString(); 
        }
    }
}
