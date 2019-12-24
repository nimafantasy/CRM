using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.GroupActivity
{
    public class GroupActivityModel
    {
        public int GrpAct_ID { get; set; }
        public string SubAct_Name { get; set; }
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public int SubAct_ID { get; set; }
        public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
    }
}