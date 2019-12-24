using Core.Domain.Common;
using Core.Infrastructure;
using Data;
using Services;
using SohaCRM.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class GroupController : Controller
    {
        GroupService _groupService;
        Active _comm;
        UserService _User;
        


        public GroupController()
        {
            _groupService = new GroupService();
            _comm = new Active();
            _User = new UserService();
        }

        [CheckSession(SubActId = 2)]
        public virtual ActionResult Index()
        {
            return RedirectToAction("Group");
        }

        [CheckSession(SubActId = 2)]
        public virtual ActionResult Group()
        {
            var model = new GroupListModel();
            model.Active = _comm.GetActiveList();
            return View(model);
        }

        [HttpPost]
        [CheckSession(SubActId = 2)]
        public virtual ActionResult GroupList(DataSourceRequest command, GroupListModel model)
        {
            if (string.IsNullOrEmpty(model.GroupName) && string.IsNullOrEmpty(model.Description) && model.IsActive_ID==0)
            {
                var GroupItems = _groupService.GetAllGroups();
                var gridModel = new DataSourceResult
                {
                    Data = GroupItems.Select(x => new GroupModel
                    {
                        GroupName = x.GroupName,
                        IsActive_ID = x.Active,
                        Group_ID = x.Group_ID,
                        IsActive = _comm.GetLiteralByInt(x.Active),
                        Description = x.Description,
                        LastUpdateUser_ID = x.LastUpdateUser_ID
                    }),
                    Total = GroupItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var UserItems = _groupService.SearchGroups(model.GroupName, model.Description, model.IsActive_ID);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new GroupModel
                    {
                        GroupName = x.GroupName,
                        Description = x.Description,
                        Group_ID = x.Group_ID,
                        LastUpdateUser_ID = x.LastUpdateUser_ID,
                        IsActive_ID = x.Active,
                        IsActive = _comm.GetLiteralByInt(x.Active)
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);
            }
        }

        [CheckSession(SubActId = 2)]
        public virtual ActionResult View(int id)
        {

            var group = _groupService.GetGroupById(id);
            if (group == null)
                //No user found with the specified id
                return RedirectToAction("List");

            var model = new GroupModel
            {
                GroupName = group.GroupName,
                Description = group.Description,
                IsActive_ID = group.Active,
                LastUpdateUser_ID = group.LastUpdateUser_ID
            };

            return View(model);
        }

        [CheckSession(SubActId = 2)]
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {

            var group = _groupService.GetGroupById(id);
            if (group == null)
                //No log found with the specified id
                return RedirectToAction("List");

            _groupService.RemoveGroup(group);


            //SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));
            return RedirectToAction("List");
        }

        [HttpPost]
        [CheckSession(SubActId = 2)]
        public ActionResult SubmitGroup(GroupModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                Tbl_Group tg = new Tbl_Group();
                tg.GroupName = model.GroupName;
                tg.Active = model.IsActive_ID;
                tg.Description = model.Description;
                tg.Group_ID = model.Group_ID;
                tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_groupService.AddNewGroup(tg))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new GroupModel
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
                        ExtraData = new GroupModel
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
                    ExtraData = new GroupModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

    }
}