using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5WordStats
{
    static class Extensions
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder textFoo = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    textFoo.Append(c);
                }
            }
            return textFoo.ToString().ToLower();
        }
    }
}
