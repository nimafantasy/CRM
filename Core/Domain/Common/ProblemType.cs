using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Domain.Common
{
    public class ProblemType
    {
        public ProblemType()
        { }

        public IEnumerable<SelectListItem> GetProblemTypeList()
        {
            try
            {
                var secs = new List<SelectListItem>();
                secs.Add(new SelectListItem() { Text = "نرم‌افزاری", Value = "1" });
                secs.Add(new SelectListItem() { Text = "سخت‌افزاری", Value = "2" });

                return new SelectList(secs, "Value", "Text");
            }
            catch (Exception ex)
            {
                var secs = new List<SelectListItem>();
                return new SelectList(secs, "Value", "Text");
            }
        }


        public string GetLiteralByInt(int number)
        {
            if (number == 1)
                return "نرم‌افزاری";
            else
                return "سخت‌افزاری";
        }


        public int GetIntByLiteral(string lit)
        {
            if (lit == "نرم‌افزاری")
                return 1;
            else
                return 2;
        }

    }
}