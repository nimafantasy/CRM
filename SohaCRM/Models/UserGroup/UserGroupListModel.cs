using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.UserGroup
{
    public class UserGroupListModel
    {
        public int GrpUser_ID { get; set; }
        public int Group_ID { get; set; }
        public int User_ID { get; set; }
        public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}