using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.Installation
{
    public class InstallationListModel
    {
        public int Installation_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerConnector { get; set; }
        public int SendSystemRequirements_ID { get; set; }
        public IEnumerable<SelectListItem> SendSystemRequirements { get; set; }
        public int InstallationLocation_ID { get; set; }
        public IEnumerable<SelectListItem> InstallationLocation { get; set; }
        public int InstallationProgram_ID { get; set; }
        public IEnumerable<SelectListItem> InstallationProgram { get; set; }
        public string DeliveryDate_SOHA { get; set; }
        public string DeliveryDate_Customer { get; set; }
        public int ClientsNumber { get; set; }
        public string PurchasedDevicesNumber { get; set; }
        public string AdministratorPassword { get; set; }
        public string IPServer { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public int SystemType_ID { get; set; }
        public IEnumerable<SelectListItem> SystemType { get; set; }
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