using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Models.Extensions
{
    public static class StringExtensions
    {
        public static string GetSubString(this string source, int size)
        {
            return source.Length >= size ? source.Substring(0, size - 1) : source;
        }
    }
}
