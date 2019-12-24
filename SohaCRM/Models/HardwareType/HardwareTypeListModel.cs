using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.HardwareType
{
    public class HardwareTypeListModel
    {
        public int Hardware_ID { get; set; }
        public string HardwareName { get; set; }
        public int Products_ID { get; set; }
        public int HardwarePrice { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public IEnumerable<SelectListItem> Product { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
    }
}