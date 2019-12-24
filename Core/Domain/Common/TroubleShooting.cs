using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Domain.Common
{
    public class TroubleShooting
    {
        public TroubleShooting()
        { }

        public IEnumerable<SelectListItem> GetTroubleShootingList()
        {
            try
            {
                var secs = new List<SelectListItem>();
                secs.Add(new SelectListItem() { Text = "مشکل بصورت تلفنی رفع شد", Value = "1" });
                secs.Add(new SelectListItem() { Text = "جهت رفع مشکل نیاز به ارسال دستگاه می‌باشد", Value = "2" });

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
                return "مشکل بصورت تلفنی رفع شد";
            else
                return "جهت رفع مشکل نیاز به ارسال دستگاه می‌باشد";
        }


        public int GetIntByLiteral(string lit)
        {
            if (lit == "مشکل بصورت تلفنی رفع شد")
                return 1;
            else
                return 2;
        }

    }
}