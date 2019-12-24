using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.GroupActivity
{
    public class GroupActivityListModel
    {
        public int GrpAct_ID { get; set; }
        public int Group_ID { get; set; }
        public int SubAct_ID { get; set; }
        public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }

    }
}