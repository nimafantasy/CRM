using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public static class Message
    {

        public static string InvalidCharacter { get { return "کارکتر نامعتبر وارد شده است"; } }
        public static string ChooseAnotherUsername { get { return "این نام کاربری قبلاً ثبت شده است"; } }
        public static string OperationSuccessful { get { return "عملیات با موفقیت انجام شد"; } }
        public static string OperationUnsuccessful { get { return "بروز خطا در انجام عملیات"; } }
        public static string OperationNotAllowed { get { return "عملیات غیرقابل انجام"; } }
        public static string StepOneSuccessful { get { return "عملیات انجام شد. ثبت نهایی نیازمند اضافه شدن محصول به مشتری است."; } }
    }
}
