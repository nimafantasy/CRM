using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models
{
    public class SubActivity
    {
        int SubAct_ID { get; set; }
        int MainAct_ID { get; set; }
        string SubActName { get; set; }
        int Active { get; set; }
    }
}