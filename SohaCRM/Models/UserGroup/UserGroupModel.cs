using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.UserGroup
{
    public class UserGroupModel
    {
        public int Group_ID { get; set; }
        public string GroupName { get; set; }



        //public int GrpUser_ID { get; set; }
        //public int Group_ID { get; set; }
        public string User { get; set; }
        public int User_ID { get; set; }
        //public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
    }
}