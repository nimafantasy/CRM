using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.SoftwareType
{
    public class SoftwareTypeModel
    {
        public int Software_ID { get; set; }
        public string Software { get; set; }
        public string SoftwareName { get; set; }
        public int Products_ID { get; set; }
        public string Product { get; set; }
        public int SoftwarePrice { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
    }
}