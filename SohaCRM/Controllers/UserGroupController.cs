using Services;
using SohaCRM.Models.AccessGroup;
using SohaCRM.Models.UserGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;
using Core.Domain.Common;


namespace SohaCRM.Controllers
{
    public class UserGroupController : Controller
    {

        UserGroupService _userGroupService;
        DepartmentService _systemService;
        UserService _userService;
        GroupService _groupService;
        Core.Domain.Common.Active _comm;

        public UserGroupController()
        {
            _userGroupService = new UserGroupService();
            //_systemService = new DepartmentService();
            _comm = new Core.Domain.Common.Active();
            _userService = new UserService();
            _groupService = new GroupService();
        }

        [CheckSession(SubActId = 4)]
        public ActionResult User_Group()
        {
            ViewBag.Title = "Home Page";
            UserGroupListModel model = new  UserGroupListModel();
            model.Users = GetUsers();
            return View(model);
        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public IEnumerable<SelectListItem> GetUsers()
        {
            var secs = _userService.GetAllUsers()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.User_ID.ToString(),
                                        Text = x.Name + " " + x.LastName
                                    });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public virtual ActionResult GroupList1(DataSourceRequest command, UserGroupListModel model)
        {

                var GroupItems = _groupService.GetAllGroups();
                var gridModel = new DataSourceResult
                {
                    Data = GroupItems.Select(x => new UserGroupModel
                    {
                        GroupName = x.GroupName,
                        Group_ID = x.Group_ID,
                    }),
                    Total = GroupItems.Count()
                };

                return Json(gridModel);
            
        }

        [CheckSession(SubActId = 4)]
        [HttpPost]
        public virtual ActionResult GroupList2(DataSourceRequest command, UserGroupListModel model)
        {

            
                var UserItems = _userGroupService.GetUserGroups(model.User_ID);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new UserGroupModel
                    {
                        GroupName = x.GroupName,
                        Group_ID = x.Group_ID
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);


        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public virtual ActionResult GetUserGroups(DataSourceRequest command, UserGroupListModel model)
        {
            var GroupItems = _userGroupService.GetUserGroups(model.User_ID);
            var gridModel = new DataSourceResult
            {
                Data = GroupItems.Select(x => new UserGroupModel
                {
                    GroupName = x.GroupName,
                    Group_ID = x.Group_ID
                }),
                Total = GroupItems.Count()
            };

            return Json(gridModel);
        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public ActionResult AddUserToGroup(DataSourceRequest command, UserGroupListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_userGroupService.AddUserToGroup(model.User_ID, model.Group_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationSuccessful,
                        MessageColor = "green",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            else
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationNotAllowed,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public ActionResult RemoveUserFromGroup(DataSourceRequest command, UserGroupListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_userGroupService.RemoveUserFromGroup(model.User_ID, model.Group_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationSuccessful,
                        MessageColor = "green",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            else
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationNotAllowed,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public ActionResult AddUserToAllGroups(DataSourceRequest command, UserGroupListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_userGroupService.AddUserToAllGroups(model.User_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationSuccessful,
                        MessageColor = "green",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            else
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationNotAllowed,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 4)]
        public ActionResult RemoveUserFromAllGroups(DataSourceRequest command, UserGroupListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_userGroupService.RemoveUserFromAllGroups(model.User_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationSuccessful,
                        MessageColor = "green",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            else
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new UserGroupListModel
                    {
                        Message = Message.OperationNotAllowed,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }
    }
}