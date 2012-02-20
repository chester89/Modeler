using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string source, params string[] elements)
        {
            return elements.Any(source.Contains);
        }

        public static bool StartsWithAny(this string source, params string[] elements)
        {
            return elements.Any(source.StartsWith);
        }
    }
}
