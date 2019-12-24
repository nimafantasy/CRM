using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Mvc;

namespace SohaCRM.Models.Customer
{
    public class CustomerListModel
    {
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerName1 { get; set; }
        public string CustomerConnector { get; set; }
        public string SubscriptionCode { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string CompanyRegistrationName { get; set; }
        public string EconomicalNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string NationalID { get; set; }
        public int CustomerField_ID { get; set; }
        public string IsActive { get; set; }
        public int IsActive_ID { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public IEnumerable<SelectListItem> Active { get; set; }
        public IEnumerable<SelectListItem> Fields { get; set; }
        public string Message { get; set; }
        public int Products_ID { get; set; }
        public string ProductsName { get; set; }

        public string MessageColor { get; set; }
    }
}