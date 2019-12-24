using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.RequestType
{
    public class RequestTypeListModel
    {
        public int RequestType_ID { get; set; }
        public string RequestTypeName { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }

    }
}