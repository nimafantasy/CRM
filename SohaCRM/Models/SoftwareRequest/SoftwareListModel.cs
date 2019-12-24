using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Mvc;

namespace SohaCRM.Models.SoftwareRequest
{
    public class SoftwareListModel
    {
        public int Request_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerConnector { get; set; }
        public int Software_ID { get; set; }
        public string RequestDate { get; set; }
        public int Amount { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryDescription { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string CustomerName { get; set; }
        public string SubscriptionCode { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string ProductsName { get; set; }
        public string SoftwareTypeDescription { get; set; }
        public string CustomerNameInSearch { get; set; }

        public bool Check_Delivery { get; set; }
        public int Status { get; set; }
        public bool Check_Revocation { get; set; }
        public string DeliveryDeviceSerialNumber { get; set; }

        public string Message { get; set; }
        public string MessageColor { get; set; }
        public IEnumerable<SelectListItem> Customer { get; set; }
        public IEnumerable<SelectListItem> Software { get; set; }
    }
}