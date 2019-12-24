using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models
{
    public class Group_Activity
    {
        int GrpAct_ID { get; set; }
        int Group_ID { get; set; }
        int SubAct_ID { get; set; }
        int Active { get; set; }
        int LastUpdateUser_ID { get; set; }
    }
}