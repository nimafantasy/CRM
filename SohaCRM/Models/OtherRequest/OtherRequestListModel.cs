using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Mvc;


namespace SohaCRM.Models.OtherRequest
{
    public class OtherRequestListModel
    {
        public int Request_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerConnector { get; set; }
        public int RequestType_ID { get; set; }
        public IEnumerable<SelectListItem> RequestType { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string CustomerName { get; set; }
        public string SubscriptionCode { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string ProductsName { get; set; }
        public string CustomerNameInSearch { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
    }
}