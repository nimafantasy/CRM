using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.DeviceFailure
{
    public class DeviceFailureListModel
    {
        public int Request_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerConnector { get; set; }
        public int ProblemType_ID { get; set; }
        public IEnumerable<SelectListItem> ProblemType { get; set; }
        public int TroubleShooting_ID { get; set; }
        public IEnumerable<SelectListItem> TroubleShooting { get; set; }
        public string RequestDate { get; set; }
        public IEnumerable<SelectListItem> DeliveryDeviceSerialNumber { get; set; }
        public int DeliveryDeviceSerialNumber_ID { get; set; }
        public string Reserve { get; set; }
        public string DeliveryDate { get; set; }
        public string WarrantyPeriod { get; set; }
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