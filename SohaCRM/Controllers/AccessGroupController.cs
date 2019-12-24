using Core.Domain.AccessGroups;
using Core.Infrastructure;
using Data;
using Services;
using SohaCRM.Models.AccessGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class AccessGroupController : Controller
    {
        //AccessGroupService _groupService;
        //DepartmentService _systemService;
        Core.Domain.Common.Active _comm;
        private readonly Services.IUser _userService;
        private readonly Services.IAccessGroup _AccessGroupService;

        public AccessGroupController()
        {
            //_groupService = new GroupService();
            //_systemService = new DepartmentService();
            _comm = new Core.Domain.Common.Active();
            _userService = EngineContext.Current.Resolve<IUser>();
            _AccessGroupService = EngineContext.Current.Resolve<IAccessGroup>();
        }

        //[CheckSession]
        public virtual ActionResult Index()
        {
            return RedirectToAction("AccessGroup");
        }

        //[CheckSession]
        public virtual ActionResult AccessGroup()
        {
            var model = new AccessGroupListModel();
            model.Active = _comm.GetActiveList();
            return View(model);
        }

        [HttpPost]
        //[CheckSession]
        public virtual ActionResult GroupList(DataSourceRequest command, AccessGroupListModel model)
        {
            //if (string.IsNullOrEmpty(model.GroupName) && string.IsNullOrEmpty(model.Description))
            {
                var GroupItems = _AccessGroupService.GetAllAccessGroups();
                var gridModel = new DataSourceResult
                {
                    Data = GroupItems.Select(x => new AccessGroupModel
                    {
                        GroupName = x.Name,
                        IsActive_ID = _comm.GetIntByBool(x.Active),
                        Group_ID = x.Id,
                        IsActive = _comm.GetLiteralByBool(x.Active),
                        Description = x.Description
                    }),
                    Total = GroupItems.Count()
                };

                return Json(gridModel);
            }
            //else
            //{
            //    var UserItems = _AccessGroupService.SearchAccessGroups(model.GroupName, model.Description);
            //    var gridModel = new DataSourceResult
            //    {
            //        Data = UserItems.Select(x => new AccessGroupModel
            //        {
            //            GroupName = x.GroupName,
            //            Description = x.Description,
            //            Group_ID = x.Group_ID,
            //            LastUpdateUser_ID = x.LastUpdateUser_ID,
            //            IsActive_ID = x.Active,
            //            IsActive = x.Active == 1 ? "فعال" : "غیرفعال"
            //        }),
            //        Total = UserItems.Count()
            //    };

            //    return Json(gridModel);
            //}
        }

        //[CheckSession]
        public virtual ActionResult View(int id)
        {

            var group = _AccessGroupService.GetAccessGroupById(id);
            if (group == null)
                //No user found with the specified id
                return RedirectToAction("List");

            var model = new AccessGroupModel
            {
                GroupName = group.Name,
                Description = group.Description,
                IsActive_ID = _comm.GetIntByBool(group.Active)
            };

            return View(model);
        }

        //[CheckSession]
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {

            var group = _AccessGroupService.GetAccessGroupById(id);
            if (group == null)
                //No log found with the specified id
                return RedirectToAction("List");

            _AccessGroupService.RemoveAccessGroup(group);


            //SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));
            return RedirectToAction("List");
        }

        [HttpPost]
        //[CheckSession]
        public string SubmitGroup(AccessGroupModel model)
        {
            //HttpSessionStateBase session = HttpContext.Session;
            //try
            //{
            //    AccessGroup tg = new AccessGroup();
            //    tg.GroupName = model.GroupName;
            //    tg.Active = model.IsActive_ID;
            //    tg.Description = model.Description;
            //    tg.Group_ID = model.Group_ID;
            //    tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);

            //    if (_AccessGroupService.AddNewAccessGroup(tg))
            //        return "Operation Successful!";
            //    else
            //        return "Operation Unsuccessful!";
            //}
            //catch (Exception ex)
            //{
            //    return "Something Went Wrong! Check System Logs.";
            //}
            return "";
        }

    }
}