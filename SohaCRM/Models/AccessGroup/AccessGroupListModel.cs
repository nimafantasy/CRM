﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.AccessGroup
{
    public class AccessGroupListModel
    {
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<SelectListItem> Active { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string IsActive { get; set; }
        public int IsActive_ID { get; set; }
    }
}