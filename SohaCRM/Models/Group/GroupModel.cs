﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.Group
{
    public class GroupModel
    {
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public string IsActive { get; set; }
        public int IsActive_ID { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
    }
}