using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.SoftwareType
{
    public class SoftwareTypeListModel
    {
        public int Software_ID { get; set; }
        public string SoftwareName { get; set; }
        public int Products_ID { get; set; }
        public int SoftwarePrice { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public IEnumerable<SelectListItem> Product { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
    }
}