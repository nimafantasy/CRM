using Core.Domain.Common;
using Core.Domain.Logs;
using Core.Domain.Users;
using Core.Infrastructure;
using Data;
using Services;
using SohaCRM.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class UserController : Controller
    {
        UserService _userService;
        //DepartmentService _systemService;
        Core.Domain.Common.Active _active;
        //private readonly Services.IUser _userService;
        DepartmentService _departmentService;
        LogService _logService;
        

        public UserController()
        {
            //_userService = new UserService();
            //_systemService = new DepartmentService();
            _active = new Active();
            //_userService = EngineContext.Current.Resolve<IUser>();
            _userService = new Services.UserService();
            _departmentService = new DepartmentService();
            _logService = new LogService();
            

        }
        [CheckSession(SubActId = 1)]
        public virtual ActionResult Index()
        {
            return RedirectToAction("User");
        }

        //[CheckSession(SubActId = 1)]
        //public virtual ActionResult User()
        //{
        //    var model = new UserListModel();

        //    model.Sections = GetSections();
        //    model.Active = _active.GetActiveList();
            
        //    return View(model);
        //}

        [CheckSession(SubActId = 1)]
        public virtual ActionResult User()
        {

            UserListModel model = new UserListModel();
                model.Sections = GetSections();
                model.Active = _active.GetActiveList();
                return View(model);
            
        }



        [CheckSession(SubActId = 1)]
        [HttpPost]
        public virtual ActionResult UserList(DataSourceRequest command, UserListModel model)
        {

            if (string.IsNullOrEmpty(model.Name) && string.IsNullOrEmpty(model.LastName) && string.IsNullOrEmpty(model.PersonelID) && model.IsActive_ID == 0 && model.Section_ID == 0)
            {
                var UserItems = _userService.GetAllUsers();
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new UserModel
                    {
                        Name = x.Name,
                        LastName = x.LastName,
                        PersonelID = x.PersonnelID.ToString(),
                        Section = _departmentService.GetDepartmentById(x.Section_ID).Section_Name,
                        UserName = x.UserName,
                        Password = x.Password,
                        IsActive_ID = x.Active,
                        Section_ID = x.Section_ID,
                        User_ID = x.User_ID,
                        IsActive = _active.GetLiteralByInt(x.Active),

                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var UserItems = _userService.SearchUsers(model.Name, model.LastName, model.PersonelID, model.UserName, model.IsActive_ID, model.Section_ID);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new UserModel
                    {
                        Name = x.Name,
                        LastName = x.LastName,
                        PersonelID = x.PersonnelID.ToString(),
                        Section = _departmentService.GetDepartmentById(x.Section_ID).Section_Name,
                        UserName = x.UserName,
                        Password = x.Password,
                        IsActive_ID = x.Active,
                        Section_ID = x.Section_ID,
                        User_ID = x.User_ID,
                        IsActive = _active.GetLiteralByInt(x.Active)



                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);
            }

        }

        //[CheckSession(SubActId = 1)]
        //public virtual ActionResult View(int id)
        //{

        //    var user = _userService.GetUserById(id);
        //    if (user == null)
        //        //No user found with the specified id
        //        return RedirectToAction("List");

        //    var model = new UserModel
        //    {
        //        Name = user.Name,
        //        LastName = user.LastName,
        //        UserName = user.UserName,
        //        Password = user.Password
        //    };

        //    return View(model);
        //}
        //[CheckSession(SubActId = 1)]
        //[HttpPost]
        //public virtual ActionResult Delete(int id)
        //{

        //    //var user = _userService.GetUserById(id);
        //    //if (user == null)
        //    //    //No log found with the specified id
        //    //    return RedirectToAction("List");

        //    //_userService.RemoveUser(user);


        //    //SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));
        //    return RedirectToAction("List");
        //}

        [CheckSession(SubActId = 1)]
        private IEnumerable<SelectListItem> GetSections()
        {

            try
            {

                var secs = _departmentService.GetAllDepartments()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.Section_ID.ToString(),
                                        Text = x.Section_Name
                                    });

                return new SelectList(secs, "Value", "Text");
            }
            catch (Exception ex)
            {
                var secs = new List<SelectListItem>();
                return new SelectList(secs, "Value", "Text");
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 1)]
        public ActionResult CheckUsername(DataSourceRequest command, UserListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            var user = _userService.GetUserById(Convert.ToInt32(@Session["UserID"]));
            if (_userService.UsernameExits(model.UserName))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserListModel
                    {
                        Message = Message.ChooseAnotherUsername,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            else
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserListModel
                    {
                        Message = "",
                        MessageColor = "green"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 1)]
        public ActionResult SubmitUser(DataSourceRequest command, UserListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            var user = _userService.GetUserById(Convert.ToInt32(@Session["UserID"]));

            try
            {
                //if (!_active.TextOnlyRegex.IsMatch(model.Name) ||
                //   !_active.TextOnlyRegex.IsMatch(model.LastName) ||
                //   !_active.NumberOnlyRegex.IsMatch(model.PersonelID))
                //{
                    
                //    var gridModel = new DataSourceResult
                //    {
                //        ExtraData = new UserListModel
                //        {
                //            Message = Message.InvalidCharacter,
                //        },
                //        Total = 1
                //    };
                //    return Json(gridModel);
                    
                //}

                Tbl_User tu = new Tbl_User();
                tu.Name = model.Name;
                tu.LastName = model.LastName;
                tu.PersonnelID = model.PersonelID;
                tu.UserName = model.UserName;
                tu.Password = model.Password;
                tu.User_ID = model.User_ID;
                tu.Active = model.IsActive_ID;
                tu.LastUpdateUser_ID =  Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                //if (tu.User_ID == 0)
                //{
                //    if (_userService.UsernameExits(tu.UserName))
                //    {
                //        var gridModel = new DataSourceResult
                //        {
                //            ExtraData = new UserListModel
                //            {
                //                Message = Message.ChooseAnotherUsername,
                //                MessageColor = "red"
                //            },
                //            Total = 1
                //        };
                //        return Json(gridModel);
                //    }
                //}

                if (_userService.AddNewUser(tu, model.Section_ID, model.IsActive_ID))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new UserListModel
                        {
                            Message = Message.OperationSuccessful,
                            MessageColor = "green"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new UserListModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }

            }
            catch (Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            //return "";
        }
    }
}