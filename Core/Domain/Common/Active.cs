using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Core.Domain.Common
{
    public class Active
    {
        public Regex TextOnlyRegex { get { return new Regex(@"^[a-zA-Z ضصيكثقفغعهخحجچپشسیبلاتنمکگظطزرژءذدئوآ]*$"); } }
        public Regex NumberOnlyRegex { get { return new Regex(@"^[0-9]*$"); } }

        public Active()
        { }

        public IEnumerable<SelectListItem> GetActiveList()
        {
            try
            {
                var secs = new List<SelectListItem>();
                secs.Add(new SelectListItem() { Text = "فعال", Value = "1" });
                secs.Add(new SelectListItem() { Text = "غیرفعال", Value = "2" });

                return new SelectList(secs, "Value", "Text");
            }
            catch (Exception ex)
            {
                var secs = new List<SelectListItem>();
                return new SelectList(secs, "Value", "Text");
            }
        }

        public bool GetBoolByInt(int number)
        {
            if (number == 1)
                return true;
            else
                return false;
        }

        public string GetLiteralByInt(int number)
        {
            if (number == 1)
                return "فعال";
            else
                return "غیرفعال";
        }

        public string GetLiteralByBool(bool number)
        {
            if (number == true)
                return "فعال";
            else
                return "غیرفعال";
        }

        public int GetIntByLiteral(string lit)
        {
            if (lit == "فعال")
                return 1;
            else
                return 2;
        }

        public int GetIntByBool(bool b)
        {
            if (b == true)
                return 1;
            else
                return 2;
        }
    }
}