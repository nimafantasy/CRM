using Core.Domain.Common;
using Services;
using SohaCRM.Models.GroupActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class GroupActivityController : Controller
    {
        GroupActivityService _groupActivityService;
        SubActivityService _subActivityService;
        DepartmentService _systemService;
        UserService _userService;
        GroupService _groupService;
        Core.Domain.Common.Active _comm;

        public GroupActivityController()
        {
            _groupActivityService = new GroupActivityService();
            //_systemService = new DepartmentService();
            _comm = new Core.Domain.Common.Active();
            _userService = new UserService();
            _groupService = new GroupService();
            _groupActivityService = new GroupActivityService();
            _subActivityService = new SubActivityService();
        }

        [CheckSession(SubActId = 3)]
        public ActionResult Group_Activity()
        {
            ViewBag.Title = "Home Page";
            GroupActivityListModel model = new GroupActivityListModel();
            model.Groups = GetGroups();
            return View(model);
        }

        [HttpPost]
        [CheckSession(SubActId = 3)]
        public IEnumerable<SelectListItem> GetGroups()
        {
            var secs = _groupService.GetAllGroups()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.Group_ID.ToString(),
                                        Text = x.GroupName
                                    });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        [CheckSession(SubActId = 3)]
        public virtual ActionResult GroupList1(DataSourceRequest command, GroupActivityListModel model)
        {

            var GroupItems = _subActivityService.GetAllSubActivities();
            var gridModel = new DataSourceResult
            {
                Data = GroupItems.Select(x => new GroupActivityModel
                {
                    SubAct_Name = x.SubActName,
                    SubAct_ID = x.SubAct_ID,
                }),
                Total = GroupItems.Count()
            };

            return Json(gridModel);

        }

        [CheckSession(SubActId = 3)]
        [HttpPost]
        public virtual ActionResult GroupList2(DataSourceRequest command, GroupActivityListModel model)
        {


            var UserItems = _groupActivityService.GetGroupSubActivity(model.Group_ID);
            var gridModel = new DataSourceResult
            {
                Data = UserItems.Select(x => new GroupActivityModel
                {
                    SubAct_Name = x.SubActName,
                    SubAct_ID = x.SubAct_ID
                }),
                Total = UserItems.Count()
            };

            return Json(gridModel);


        }

        [HttpPost]
        [CheckSession(SubActId = 3)]
        public virtual ActionResult GetGroupActivities(DataSourceRequest command, GroupActivityListModel model)
        {
            var GroupItems = _groupActivityService.GetGroupSubActivity(model.Group_ID);
            var gridModel = new DataSourceResult
            {
                Data = GroupItems.Select(x => new GroupActivityModel
                {
                     SubAct_Name = x.SubActName,
                    SubAct_ID = x.SubAct_ID
                }),
                Total = GroupItems.Count()
            };

            return Json(gridModel);
        }

        [HttpPost]
        [CheckSession(SubActId = 3)]
        public ActionResult AddSubActivityToGroup(DataSourceRequest command, GroupActivityListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_groupActivityService.AddSubActivityToGroup(model.SubAct_ID, model.Group_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new GroupActivityListModel
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
                    ExtraData = new GroupActivityListModel
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
        [CheckSession(SubActId = 3)]
        public ActionResult RemoveActivityFromGroup(DataSourceRequest command, GroupActivityListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_groupActivityService.RemoveSubActivityFromGroup(model.SubAct_ID, model.Group_ID, Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new GroupActivityListModel
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
                    ExtraData = new GroupActivityListModel
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
        [CheckSession(SubActId = 3)]
        public ActionResult AddSubactivitiesToAllGroups(DataSourceRequest command, GroupActivityListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_groupActivityService.AddSubActivityToAllGroups(model.Group_ID, Convert.ToInt32(session["UserID"])))
            {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new GroupActivityListModel
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
                        ExtraData = new GroupActivityListModel
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
        [CheckSession(SubActId = 3)]
        public ActionResult RemoveSubActivityFromAllGroups(DataSourceRequest command, GroupActivityListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            if (_groupActivityService.RemoveSubActivityFromAllGroups(model.Group_ID,  Convert.ToInt32(session["UserID"])))
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new GroupActivityListModel
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
                    ExtraData = new GroupActivityListModel
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