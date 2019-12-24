using Data;
using SohaCRM.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View("index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    
        public ActionResult User()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Group_Activity()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Customer_Products()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Hardware_Req()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Software_Req()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ChangeSoftware_Req()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Other_Req()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult RequestType()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult SupportServices()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult DeviceFailure()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult SendToRepair()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ReturnRepairedDevice()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Installation()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult CustomerField()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Products()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult HardwareType()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult SoftwareType()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UnderConstruction()
        {
            ViewBag.Title = "در دست ساخت";

            return View();
        }

        public ActionResult ActivitySuspended()
        {
            ViewBag.Title = "محدودیت فعالیت";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel objUser)
        {
            objUser.Message = "";
            if (ModelState.IsValid)
            {
                using (CRMEntities db = new CRMEntities())
                {

                    var obj = db.Tbl_User.Where(a => a.UserName.Equals(objUser.UserName)).FirstOrDefault();
                    if (obj != null)
                    {
                        if (CompareStrings(obj.Password, objUser.Password))
                        {
                            Session["UserID"] = obj.User_ID.ToString();
                            Session["Username"] = obj.UserName.ToString();
                            return RedirectToAction("index");
                        }
                        else
                        {
                            objUser.Message = ".نام کاربری یا رمز عبور صحیح نمیباشد";
                        }
                    }
                    else
                    {
                        objUser.Message = ".نام کاربری یا رمز عبور صحیح نمیباشد";
                    }
                }
            }
            return View(objUser);
        }

        private bool CompareStrings(string a, string b)
        {
            if (String.CompareOrdinal(a, b).Equals(0))
                return true;
            else
                return false;
        }


    }
}
