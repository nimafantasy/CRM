using Core.Domain.Common;
using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Framework
{
    public class CheckSession : ActionFilterAttribute
    {
        public int SubActId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                UserService _userService = new UserService();
                Active _active = new Active();
                var user = _userService.GetUserById(Convert.ToInt32(session["UserID"]));
                List<Tbl_Group> grps = _userService.GetUserGroups(Convert.ToInt32(session["UserID"]));

                if (SubActId != 0)
                {

                    if (!grps.Any(x => x.Tbl_Group_Activity.Any(y => y.SubAct_ID.Equals(SubActId))))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "index" }
                            });
                    }

                    
                }

                if (user.Active != _active.GetIntByLiteral("فعال")) // user not active
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "ActivitySuspended" }
                                });
                }

                if (session != null && session["UserName"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                                    });
                }
            }
            catch(Exception ex)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                                    });
            }

        }
    }
}
