using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.HardwareType
{
    public class HardwareTypeModel
    {
        public int Hardware_ID { get; set; }
        public string Hardware { get; set; }
        public string HardwareName { get; set; }
        public int Products_ID { get; set; }
        public string Product { get; set; }
        public int HardwarePrice { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
    }
}