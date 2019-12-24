using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.ReturnRepairedDevice
{
    public class ReturnRepairedDeviceListModel
    {
        public int Request_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerConnector { get; set; }
        public int DeviceSerialNumber_ID { get; set; }
        public IEnumerable<SelectListItem> DeviceSerialNumber { get; set; }
        public string Reserve { get; set; }
        public string DeliveryDate { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Repairs { get; set; }
        public string ReturnDate { get; set; }
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