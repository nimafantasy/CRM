using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public static class RegExx
    {
        public static Regex TextOnlyRegex { get { return new Regex(@"^[a-zA-Z ضصثقفغعهخحجچپشسیبلاتنمکگظطزرذدئوآ]*$"); } }
        public static Regex NumberOnlyRegex { get { return new Regex(@"^[0-9]*$"); } }
    }
}
